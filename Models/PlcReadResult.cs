namespace mes_plc.Models;

public sealed class PlcReadResult
{
    public DateTime Timestamp { get; init; } = DateTime.Now;

    public string Vendor { get; init; } = string.Empty;

    public string IpAddress { get; init; } = string.Empty;

    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public Dictionary<string, object> Values { get; init; } = [];
}
