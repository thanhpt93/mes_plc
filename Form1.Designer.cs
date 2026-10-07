namespace mes_plc
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabMain = new TabControl();
            tabConfigTest = new TabPage();
            txtReadAddresses = new TextBox();
            lblReadAddresses = new Label();
            chkAutoStart = new CheckBox();
            chkStartWithWindows = new CheckBox();
            txtTestOutput = new TextBox();
            lblTestOutput = new Label();
            btnSaveConfig = new Button();
            btnTestRead = new Button();
            numPollingInterval = new NumericUpDown();
            lblPollingInterval = new Label();
            numPort = new NumericUpDown();
            lblPort = new Label();
            txtIpAddress = new TextBox();
            lblIpAddress = new Label();
            cmbVendor = new ComboBox();
            lblVendor = new Label();
            tabProduction = new TabPage();
            txtProductionLog = new TextBox();
            lblProductionLog = new Label();
            btnStopService = new Button();
            btnStartService = new Button();
            lblServiceStatusValue = new Label();
            lblServiceStatus = new Label();
            trayIcon = new NotifyIcon(components);
            trayMenu = new ContextMenuStrip(components);
            menuShow = new ToolStripMenuItem();
            menuStart = new ToolStripMenuItem();
            menuStop = new ToolStripMenuItem();
            menuExit = new ToolStripMenuItem();
            tabMain.SuspendLayout();
            tabConfigTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPollingInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            tabProduction.SuspendLayout();
            trayMenu.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Controls.Add(tabConfigTest);
            tabMain.Controls.Add(tabProduction);
            tabMain.Dock = DockStyle.Fill;
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(984, 561);
            tabMain.TabIndex = 0;
            // 
            // tabConfigTest
            // 
            tabConfigTest.Controls.Add(txtReadAddresses);
            tabConfigTest.Controls.Add(lblReadAddresses);
            tabConfigTest.Controls.Add(chkAutoStart);
            tabConfigTest.Controls.Add(chkStartWithWindows);
            tabConfigTest.Controls.Add(txtTestOutput);
            tabConfigTest.Controls.Add(lblTestOutput);
            tabConfigTest.Controls.Add(btnSaveConfig);
            tabConfigTest.Controls.Add(btnTestRead);
            tabConfigTest.Controls.Add(numPollingInterval);
            tabConfigTest.Controls.Add(lblPollingInterval);
            tabConfigTest.Controls.Add(numPort);
            tabConfigTest.Controls.Add(lblPort);
            tabConfigTest.Controls.Add(txtIpAddress);
            tabConfigTest.Controls.Add(lblIpAddress);
            tabConfigTest.Controls.Add(cmbVendor);
            tabConfigTest.Controls.Add(lblVendor);
            tabConfigTest.Location = new Point(4, 24);
            tabConfigTest.Name = "tabConfigTest";
            tabConfigTest.Padding = new Padding(12);
            tabConfigTest.Size = new Size(976, 533);
            tabConfigTest.TabIndex = 0;
            tabConfigTest.Text = "Config && Test";
            tabConfigTest.UseVisualStyleBackColor = true;
            // 
            // txtReadAddresses
            // 
            txtReadAddresses.Location = new Point(188, 160);
            txtReadAddresses.Multiline = true;
            txtReadAddresses.Name = "txtReadAddresses";
            txtReadAddresses.ScrollBars = ScrollBars.Vertical;
            txtReadAddresses.Size = new Size(420, 95);
            txtReadAddresses.TabIndex = 15;
            // 
            // lblReadAddresses
            // 
            lblReadAddresses.AutoSize = true;
            lblReadAddresses.Location = new Point(20, 163);
            lblReadAddresses.Name = "lblReadAddresses";
            lblReadAddresses.Size = new Size(145, 15);
            lblReadAddresses.TabIndex = 14;
            lblReadAddresses.Text = "Read addresses (1/line)";
            // 
            // chkAutoStart
            // 
            chkAutoStart.AutoSize = true;
            chkAutoStart.Location = new Point(26, 293);
            chkAutoStart.Name = "chkAutoStart";
            chkAutoStart.Size = new Size(230, 19);
            chkAutoStart.TabIndex = 13;
            chkAutoStart.Text = "Auto start Production mode on launch";
            chkAutoStart.UseVisualStyleBackColor = true;
            // 
            // chkStartWithWindows
            // 
            chkStartWithWindows.AutoSize = true;
            chkStartWithWindows.Location = new Point(26, 268);
            chkStartWithWindows.Name = "chkStartWithWindows";
            chkStartWithWindows.Size = new Size(151, 19);
            chkStartWithWindows.TabIndex = 12;
            chkStartWithWindows.Text = "Start app with Windows";
            chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // txtTestOutput
            // 
            txtTestOutput.Location = new Point(26, 397);
            txtTestOutput.Multiline = true;
            txtTestOutput.Name = "txtTestOutput";
            txtTestOutput.ReadOnly = true;
            txtTestOutput.ScrollBars = ScrollBars.Vertical;
            txtTestOutput.Size = new Size(924, 103);
            txtTestOutput.TabIndex = 11;
            // 
            // lblTestOutput
            // 
            lblTestOutput.AutoSize = true;
            lblTestOutput.Location = new Point(20, 379);
            lblTestOutput.Name = "lblTestOutput";
            lblTestOutput.Size = new Size(67, 15);
            lblTestOutput.TabIndex = 10;
            lblTestOutput.Text = "Test output";
            // 
            // btnSaveConfig
            // 
            btnSaveConfig.Location = new Point(26, 329);
            btnSaveConfig.Name = "btnSaveConfig";
            btnSaveConfig.Size = new Size(143, 34);
            btnSaveConfig.TabIndex = 9;
            btnSaveConfig.Text = "Save Config";
            btnSaveConfig.UseVisualStyleBackColor = true;
            btnSaveConfig.Click += BtnSaveConfig_Click;
            // 
            // btnTestRead
            // 
            btnTestRead.Location = new Point(175, 329);
            btnTestRead.Name = "btnTestRead";
            btnTestRead.Size = new Size(143, 34);
            btnTestRead.TabIndex = 8;
            btnTestRead.Text = "Test Read";
            btnTestRead.UseVisualStyleBackColor = true;
            btnTestRead.Click += BtnTestRead_Click;
            // 
            // numPollingInterval
            // 
            numPollingInterval.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            numPollingInterval.Location = new Point(188, 126);
            numPollingInterval.Maximum = new decimal(new int[] { 60000, 0, 0, 0 });
            numPollingInterval.Minimum = new decimal(new int[] { 200, 0, 0, 0 });
            numPollingInterval.Name = "numPollingInterval";
            numPollingInterval.Size = new Size(180, 23);
            numPollingInterval.TabIndex = 7;
            numPollingInterval.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // lblPollingInterval
            // 
            lblPollingInterval.AutoSize = true;
            lblPollingInterval.Location = new Point(20, 128);
            lblPollingInterval.Name = "lblPollingInterval";
            lblPollingInterval.Size = new Size(113, 15);
            lblPollingInterval.TabIndex = 6;
            lblPollingInterval.Text = "Polling interval (ms)";
            // 
            // numPort
            // 
            numPort.Location = new Point(188, 88);
            numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(180, 23);
            numPort.TabIndex = 5;
            numPort.Value = new decimal(new int[] { 102, 0, 0, 0 });
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(20, 90);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 4;
            lblPort.Text = "Port";
            // 
            // txtIpAddress
            // 
            txtIpAddress.Location = new Point(188, 52);
            txtIpAddress.Name = "txtIpAddress";
            txtIpAddress.Size = new Size(280, 23);
            txtIpAddress.TabIndex = 3;
            // 
            // lblIpAddress
            // 
            lblIpAddress.AutoSize = true;
            lblIpAddress.Location = new Point(20, 55);
            lblIpAddress.Name = "lblIpAddress";
            lblIpAddress.Size = new Size(62, 15);
            lblIpAddress.TabIndex = 2;
            lblIpAddress.Text = "IP Address";
            // 
            // cmbVendor
            // 
            cmbVendor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVendor.FormattingEnabled = true;
            cmbVendor.Location = new Point(188, 15);
            cmbVendor.Name = "cmbVendor";
            cmbVendor.Size = new Size(280, 23);
            cmbVendor.TabIndex = 1;
            // 
            // lblVendor
            // 
            lblVendor.AutoSize = true;
            lblVendor.Location = new Point(20, 18);
            lblVendor.Name = "lblVendor";
            lblVendor.Size = new Size(44, 15);
            lblVendor.TabIndex = 0;
            lblVendor.Text = "Vendor";
            // 
            // tabProduction
            // 
            tabProduction.Controls.Add(txtProductionLog);
            tabProduction.Controls.Add(lblProductionLog);
            tabProduction.Controls.Add(btnStopService);
            tabProduction.Controls.Add(btnStartService);
            tabProduction.Controls.Add(lblServiceStatusValue);
            tabProduction.Controls.Add(lblServiceStatus);
            tabProduction.Location = new Point(4, 24);
            tabProduction.Name = "tabProduction";
            tabProduction.Padding = new Padding(12);
            tabProduction.Size = new Size(976, 533);
            tabProduction.TabIndex = 1;
            tabProduction.Text = "Production";
            tabProduction.UseVisualStyleBackColor = true;
            // 
            // txtProductionLog
            // 
            txtProductionLog.Location = new Point(18, 108);
            txtProductionLog.Multiline = true;
            txtProductionLog.Name = "txtProductionLog";
            txtProductionLog.ReadOnly = true;
            txtProductionLog.ScrollBars = ScrollBars.Vertical;
            txtProductionLog.Size = new Size(938, 392);
            txtProductionLog.TabIndex = 5;
            // 
            // lblProductionLog
            // 
            lblProductionLog.AutoSize = true;
            lblProductionLog.Location = new Point(18, 80);
            lblProductionLog.Name = "lblProductionLog";
            lblProductionLog.Size = new Size(86, 15);
            lblProductionLog.TabIndex = 4;
            lblProductionLog.Text = "Production log";
            // 
            // btnStopService
            // 
            btnStopService.Enabled = false;
            btnStopService.Location = new Point(169, 39);
            btnStopService.Name = "btnStopService";
            btnStopService.Size = new Size(143, 34);
            btnStopService.TabIndex = 3;
            btnStopService.Text = "Stop Service";
            btnStopService.UseVisualStyleBackColor = true;
            btnStopService.Click += BtnStopService_Click;
            // 
            // btnStartService
            // 
            btnStartService.Location = new Point(18, 39);
            btnStartService.Name = "btnStartService";
            btnStartService.Size = new Size(143, 34);
            btnStartService.TabIndex = 2;
            btnStartService.Text = "Start Service";
            btnStartService.UseVisualStyleBackColor = true;
            btnStartService.Click += BtnStartService_Click;
            // 
            // lblServiceStatusValue
            // 
            lblServiceStatusValue.AutoSize = true;
            lblServiceStatusValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblServiceStatusValue.Location = new Point(82, 12);
            lblServiceStatusValue.Name = "lblServiceStatusValue";
            lblServiceStatusValue.Size = new Size(37, 15);
            lblServiceStatusValue.TabIndex = 1;
            lblServiceStatusValue.Text = "STOP";
            // 
            // lblServiceStatus
            // 
            lblServiceStatus.AutoSize = true;
            lblServiceStatus.Location = new Point(18, 12);
            lblServiceStatus.Name = "lblServiceStatus";
            lblServiceStatus.Size = new Size(42, 15);
            lblServiceStatus.TabIndex = 0;
            lblServiceStatus.Text = "Status:";
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = trayMenu;
            trayIcon.Text = "MES PLC Collector";
            trayIcon.Visible = true;
            // 
            // trayMenu
            // 
            trayMenu.ImageScalingSize = new Size(24, 24);
            trayMenu.Items.AddRange(new ToolStripItem[] { menuShow, menuStart, menuStop, menuExit });
            trayMenu.Name = "trayMenu";
            trayMenu.Size = new Size(104, 92);
            // 
            // menuShow
            // 
            menuShow.Name = "menuShow";
            menuShow.Size = new Size(103, 22);
            menuShow.Text = "Show";
            // 
            // menuStart
            // 
            menuStart.Name = "menuStart";
            menuStart.Size = new Size(103, 22);
            menuStart.Text = "Start";
            // 
            // menuStop
            // 
            menuStop.Name = "menuStop";
            menuStop.Size = new Size(103, 22);
            menuStop.Text = "Stop";
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(103, 22);
            menuExit.Text = "Exit";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(tabMain);
            Name = "Form1";
            Text = "MES PLC Collector";
            tabMain.ResumeLayout(false);
            tabConfigTest.ResumeLayout(false);
            tabConfigTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPollingInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            tabProduction.ResumeLayout(false);
            tabProduction.PerformLayout();
            trayMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabMain;
        private TabPage tabConfigTest;
        private TabPage tabProduction;
        private TextBox txtReadAddresses;
        private Label lblReadAddresses;
        private ComboBox cmbVendor;
        private Label lblVendor;
        private TextBox txtIpAddress;
        private Label lblIpAddress;
        private NumericUpDown numPort;
        private Label lblPort;
        private NumericUpDown numPollingInterval;
        private Label lblPollingInterval;
        private Button btnTestRead;
        private Button btnSaveConfig;
        private TextBox txtTestOutput;
        private Label lblTestOutput;
        private CheckBox chkStartWithWindows;
        private CheckBox chkAutoStart;
        private Label lblServiceStatus;
        private Label lblServiceStatusValue;
        private Button btnStartService;
        private Button btnStopService;
        private TextBox txtProductionLog;
        private Label lblProductionLog;
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private ToolStripMenuItem menuShow;
        private ToolStripMenuItem menuStart;
        private ToolStripMenuItem menuStop;
        private ToolStripMenuItem menuExit;
    }
}
