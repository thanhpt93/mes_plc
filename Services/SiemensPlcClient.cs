using mes_plc.Models;
using S7.Net;

namespace mes_plc.Services;

public sealed class SiemensPlcClient : IPlcClient
{
    public PlcVendor Vendor => PlcVendor.Siemens;

    private static readonly string[] DefaultAddresses = ["DB1.DBW0", "DB1.DBW2", "M0.0"];

    public Task<PlcReadResult> TestReadAsync(PlcConfig config, CancellationToken cancellationToken)
    {
        return ReadAsync(config, cancellationToken, "TEST");
    }

    public Task<PlcReadResult> ReadProductionAsync(PlcConfig config, CancellationToken cancellationToken)
    {
        return ReadAsync(config, cancellationToken, "RUN");
    }

    private static Task<PlcReadResult> ReadAsync(PlcConfig config, CancellationToken cancellationToken, string mode)
    {
        return Task.Run(() =>
        {
            var addresses = ResolveAddresses(config, DefaultAddresses);
            var values = new Dictionary<string, object>();

            try
            {
                using var plc = new Plc(CpuType.S71200, config.IpAddress, config.Port, 0, 1);
                plc.Open();

                if (!plc.IsConnected)
                {
                    return BuildResult(config, false, values, $"{mode}: Cannot connect to Siemens PLC {config.IpAddress}:{config.Port}");
                }

                foreach (var address in addresses)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    try
                    {
                        var value = plc.Read(address);
                        values[address] = value ?? "null";
                    }
                    catch (Exception ex)
                    {
                        values[address] = $"ERR: {ex.Message}";
                    }
                }

                var success = values.Values.Any(v => !v.ToString()!.StartsWith("ERR:"));
                return BuildResult(config, success, values, success ? $"{mode}: Siemens read completed" : $"{mode}: Siemens read failed for all addresses");
            }
            catch (Exception ex)
            {
                return BuildResult(config, false, values, $"{mode}: Siemens error - {ex.Message}");
            }
        }, cancellationToken);
    }

    private static PlcReadResult BuildResult(PlcConfig config, bool success, Dictionary<string, object> values, string message)
    {
        return new PlcReadResult
        {
            Vendor = PlcVendor.Siemens.ToString(),
            IpAddress = config.IpAddress,
            Success = success,
            Message = message,
            Values = values
        };
    }

    private static IReadOnlyList<string> ResolveAddresses(PlcConfig config, IReadOnlyList<string> fallback)
    {
        if (config.ReadAddresses.Count == 0)
        {
            return fallback;
        }

        return config.ReadAddresses;
    }
}
