using mes_plc.Models;
using OmronFinsTCP.Net;

namespace mes_plc.Services;

public sealed class OmronPlcClient : IPlcClient
{
    public PlcVendor Vendor => PlcVendor.Omron;

    private static readonly string[] DefaultAddresses = ["DM100", "CIO0.00", "WR10"];

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
            var plc = new EtherNetPLC();

            try
            {
                var connectCode = plc.Link(config.IpAddress, (short)config.Port, 2000);
                if (connectCode != 0)
                {
                    return BuildResult(config, false, values, $"{mode}: Omron connect failed - code {connectCode}");
                }

                foreach (var address in addresses)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (!TryParseAddress(address, out var memory, out var wordAddress, out var bitAddress))
                    {
                        values[address] = "ERR: Invalid Omron address format";
                        continue;
                    }

                    if (bitAddress is not null)
                    {
                        var code = plc.GetBitState(memory, bitAddress, out var bitValue);
                        values[address] = code == 0 ? bitValue == 1 : $"ERR: Code {code}";
                    }
                    else
                    {
                        var code = plc.ReadWord(memory, wordAddress, out var word);
                        values[address] = code == 0 ? word : $"ERR: Code {code}";
                    }
                }

                var success = values.Values.Any(v => !v.ToString()!.StartsWith("ERR:"));
                return BuildResult(config, success, values, success ? $"{mode}: Omron read completed" : $"{mode}: Omron read failed for all addresses");
            }
            catch (Exception ex)
            {
                return BuildResult(config, false, values, $"{mode}: Omron error - {ex.Message}");
            }
            finally
            {
                plc.Close();
            }
        }, cancellationToken);
    }

    private static bool TryParseAddress(string rawAddress, out PlcMemory memory, out short wordAddress, out string? bitAddress)
    {
        memory = PlcMemory.DM;
        wordAddress = 0;
        bitAddress = null;

        if (string.IsNullOrWhiteSpace(rawAddress))
        {
            return false;
        }

        var address = rawAddress.Trim().ToUpperInvariant();

        string prefix;
        if (address.StartsWith("CIO", StringComparison.Ordinal))
        {
            memory = PlcMemory.CIO;
            prefix = "CIO";
        }
        else if (address.StartsWith("WR", StringComparison.Ordinal))
        {
            memory = PlcMemory.WR;
            prefix = "WR";
        }
        else if (address.StartsWith("DM", StringComparison.Ordinal))
        {
            memory = PlcMemory.DM;
            prefix = "DM";
        }
        else
        {
            return false;
        }

        var body = address[prefix.Length..];
        if (body.Contains('.', StringComparison.Ordinal))
        {
            bitAddress = body;
            var wordPart = body.Split('.')[0];
            return short.TryParse(wordPart, out wordAddress);
        }

        return short.TryParse(body, out wordAddress);
    }

    private static PlcReadResult BuildResult(PlcConfig config, bool success, Dictionary<string, object> values, string message)
    {
        return new PlcReadResult
        {
            Vendor = PlcVendor.Omron.ToString(),
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
