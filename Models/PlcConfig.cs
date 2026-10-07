namespace mes_plc.Models;

public sealed class PlcConfig
{
    public PlcVendor Vendor { get; set; } = PlcVendor.Siemens;

    public string IpAddress { get; set; } = "192.168.0.1";

    public int Port { get; set; } = 102;

    public int PollingIntervalMs { get; set; } = 1000;

    public List<string> ReadAddresses { get; set; } = [];

    public bool AutoStartProductionOnLaunch { get; set; }

    public bool StartWithWindows { get; set; }
}
