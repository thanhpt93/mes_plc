using mes_plc.Models;
using mes_plc.Services;

namespace mes_plc;

public partial class Form1 : Form
{
    private readonly IConfigService _configService;
    private readonly IStartupRegistrationService _startupRegistrationService;
    private readonly ICollectorService _collectorService;
    private readonly IPlcClientFactory _plcClientFactory;
    private readonly SemaphoreSlim _serviceLock = new(1, 1);
    private readonly string _testReadLogFilePath = Path.Combine(Path.GetTempPath(), "mes_plc_test_read.log");

    private PlcConfig _currentConfig = new();
    private CancellationTokenSource? _serviceCts;
    private bool _isServiceRunning;
    private bool _isExitRequested;

    public Form1()
    {
        InitializeComponent();

        _configService = new ConfigService();
        _startupRegistrationService = new StartupRegistrationService();
        _plcClientFactory = new PlcClientFactory();
        _collectorService = new CollectorService(_plcClientFactory);

        ConfigureForm();
        WireEvents();
    }

    private void ConfigureForm()
    {
        cmbVendor.DataSource = Enum.GetValues<PlcVendor>();
        trayIcon.Icon = SystemIcons.Application;
        trayIcon.Visible = true;
    }

    private void WireEvents()
    {
        Shown += Form1_Shown;
        FormClosing += Form1_FormClosing;
        Resize += Form1_Resize;

        btnSaveConfig.Click += BtnSaveConfig_Click;
        btnTestRead.Click += BtnTestRead_Click;
        btnStartService.Click += BtnStartService_Click;
        btnStopService.Click += BtnStopService_Click;

        menuShow.Click += (_, _) => ShowFromTray();
        menuStart.Click += async (_, _) => await StartServiceFromUiAsync();
        menuStop.Click += async (_, _) => await StopServiceAsync();
        menuExit.Click += async (_, _) => await ExitApplicationAsync();
        trayIcon.DoubleClick += (_, _) => ShowFromTray();
    }

    private async void Form1_Shown(object? sender, EventArgs e)
    {
        _currentConfig = await _configService.LoadAsync(CancellationToken.None);
        ApplyConfigToControls(_currentConfig);
        AppendTestLog($"Test log file: {_testReadLogFilePath}");

        if (_currentConfig.AutoStartProductionOnLaunch)
        {
            await StartServiceFromUiAsync();
        }
    }

    private void ApplyConfigToControls(PlcConfig config)
    {
        cmbVendor.SelectedItem = config.Vendor;
        txtIpAddress.Text = config.IpAddress;
        numPort.Value = Math.Clamp(config.Port, (int)numPort.Minimum, (int)numPort.Maximum);
        numPollingInterval.Value = Math.Clamp(config.PollingIntervalMs, (int)numPollingInterval.Minimum, (int)numPollingInterval.Maximum);
        txtReadAddresses.Text = string.Join(Environment.NewLine, config.ReadAddresses);
        chkAutoStart.Checked = config.AutoStartProductionOnLaunch;
        chkStartWithWindows.Checked = config.StartWithWindows || _startupRegistrationService.IsRegistered();
    }

    private PlcConfig ReadConfigFromControls()
    {
        var selectedVendor = cmbVendor.SelectedItem is PlcVendor vendor ? vendor : PlcVendor.Siemens;
        return new PlcConfig
        {
            Vendor = selectedVendor,
            IpAddress = txtIpAddress.Text.Trim(),
            Port = (int)numPort.Value,
            PollingIntervalMs = (int)numPollingInterval.Value,
            ReadAddresses = ParseAddressesFromInput(txtReadAddresses.Text),
            AutoStartProductionOnLaunch = chkAutoStart.Checked,
            StartWithWindows = chkStartWithWindows.Checked
        };
    }

    private static List<string> ParseAddressesFromInput(string input)
    {
        return input
            .Split(['\r', '\n', ',', ';'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private async void BtnSaveConfig_Click(object? sender, EventArgs e)
    {
        _currentConfig = ReadConfigFromControls();
        await _configService.SaveAsync(_currentConfig, CancellationToken.None);
        _startupRegistrationService.SetRegistration(_currentConfig.StartWithWindows);
        AppendTestLog("Configuration saved.");
    }

    private async void BtnTestRead_Click(object? sender, EventArgs e)
    {
        btnTestRead.Enabled = false;
        AppendTestLog("Starting test read...");

        try
        {
            var config = ReadConfigFromControls();
            var client = _plcClientFactory.Create(config.Vendor);

            var result = await Task.Run(() => client.TestReadAsync(config, CancellationToken.None));
            AppendTestLog(FormatResult(result));
        }
        catch (Exception ex)
        {
            AppendTestLog($"Test error: {ex.Message}");
        }
        finally
        {
            btnTestRead.Enabled = true;
        }
    }

    private async void BtnStartService_Click(object? sender, EventArgs e)
    {
        await StartServiceFromUiAsync();
    }

    private async Task StartServiceFromUiAsync()
    {
        await _serviceLock.WaitAsync();
        try
        {
            if (_isServiceRunning)
            {
                return;
            }

            _currentConfig = ReadConfigFromControls();
            await _configService.SaveAsync(_currentConfig, CancellationToken.None);
            _startupRegistrationService.SetRegistration(_currentConfig.StartWithWindows);

            _serviceCts = new CancellationTokenSource();
            _isServiceRunning = true;
            UpdateServiceUiState(true);
            AppendProductionLog("Service started.");

            _ = Task.Run(() => _collectorService.StartAsync(
                    _currentConfig,
                    result => BeginInvoke(() => AppendProductionLog(FormatResult(result))),
                    error => BeginInvoke(() => AppendProductionLog($"Service error: {error.Message}")),
                    _serviceCts.Token),
                _serviceCts.Token);

            HideToTray();
        }
        finally
        {
            _serviceLock.Release();
        }
    }

    private async void BtnStopService_Click(object? sender, EventArgs e)
    {
        await StopServiceAsync();
    }

    private async Task StopServiceAsync()
    {
        await _serviceLock.WaitAsync();
        try
        {
            if (!_isServiceRunning)
            {
                return;
            }

            _serviceCts?.Cancel();
            _serviceCts?.Dispose();
            _serviceCts = null;

            _isServiceRunning = false;
            UpdateServiceUiState(false);
            AppendProductionLog("Service stopped.");
        }
        finally
        {
            _serviceLock.Release();
        }
    }

    private static string FormatResult(PlcReadResult result)
    {
        var values = result.Values.Count == 0
            ? "(no values)"
            : string.Join(", ", result.Values.Select(x => $"{x.Key}={x.Value}"));

        return $"[{result.Timestamp:HH:mm:ss}] {result.Vendor}@{result.IpAddress} | {(result.Success ? "OK" : "FAIL")} | {result.Message} | {values}";
    }

    private void UpdateServiceUiState(bool running)
    {
        lblServiceStatusValue.Text = running ? "RUNNING" : "STOP";
        btnStartService.Enabled = !running;
        btnStopService.Enabled = running;
        menuStart.Enabled = !running;
        menuStop.Enabled = running;
    }

    private void AppendTestLog(string message)
    {
        var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        txtTestOutput.AppendText($"{line}{Environment.NewLine}");

        _ = Task.Run(() =>
        {
            try
            {
                File.AppendAllText(_testReadLogFilePath, line + Environment.NewLine);
            }
            catch
            {
            }
        });
    }

    private void AppendProductionLog(string message)
    {
        txtProductionLog.AppendText($"{message}{Environment.NewLine}");
    }

    private void HideToTray()
    {
        WindowState = FormWindowState.Minimized;
        ShowInTaskbar = false;
        Hide();
        trayIcon.ShowBalloonTip(1500, "MES PLC Collector", "Production mode is running in the background.", ToolTipIcon.Info);
    }

    private void ShowFromTray()
    {
        Show();
        WindowState = FormWindowState.Normal;
        ShowInTaskbar = true;
        Activate();
    }

    private async void Form1_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_isExitRequested)
        {
            await StopServiceAsync();
            trayIcon.Visible = false;
            return;
        }

        if (_isServiceRunning)
        {
            e.Cancel = true;
            HideToTray();
        }
    }

    private void Form1_Resize(object? sender, EventArgs e)
    {
        if (WindowState == FormWindowState.Minimized && _isServiceRunning)
        {
            HideToTray();
        }
    }

    private async Task ExitApplicationAsync()
    {
        _isExitRequested = true;
        await StopServiceAsync();
        Close();
    }
}
