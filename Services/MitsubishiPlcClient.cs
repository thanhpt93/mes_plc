using HslCommunication.Profinet.Melsec;
using mes_plc.Models;

namespace mes_plc.Services;

public sealed class MitsubishiPlcClient : IPlcClient
{
    public PlcVendor Vendor => PlcVendor.Mitsubishi;

    private static readonly string[] DefaultAddresses = ["D0", "D10", "M100"];

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

            using var plc = new MelsecMcNet(config.IpAddress, config.Port);

            try
            {
                var connectResult = plc.ConnectServer();
                if (!connectResult.IsSuccess)
                {
                    return BuildResult(config, false, values, $"{mode}: Mitsubishi connect failed - {connectResult.Message}");
                }

                foreach (var address in addresses)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (IsBitAddress(address))
                    {
                        var readBool = plc.ReadBool(address);
                        values[address] = readBool.IsSuccess ? readBool.Content : $"ERR: {readBool.Message}";
                    }
                    else
                    {
                        var readWord = plc.ReadInt16(address);
                        values[address] = readWord.IsSuccess ? readWord.Content : $"ERR: {readWord.Message}";
                    }
                }

                var success = values.Values.Any(v => !v.ToString()!.StartsWith("ERR:"));
                return BuildResult(config, success, values, success ? $"{mode}: Mitsubishi read completed" : $"{mode}: Mitsubishi read failed for all addresses");
            }
            catch (Exception ex)
            {
                return BuildResult(config, false, values, $"{mode}: Mitsubishi error - {ex.Message}");
            }
            finally
            {
                plc.ConnectClose();
            }
        }, cancellationToken);
    }

    private static bool IsBitAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return false;
        }

        var leading = char.ToUpperInvariant(address.Trim()[0]);
        return leading is 'M' or 'X' or 'Y' or 'B' or 'S';
    }

    private static PlcReadResult BuildResult(PlcConfig config, bool success, Dictionary<string, object> values, string message)
    {
        return new PlcReadResult
        {
            Vendor = PlcVendor.Mitsubishi.ToString(),
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
