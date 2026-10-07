using System.Net.Sockets;
using mes_plc.Models;

namespace mes_plc.Services;

public abstract class BasePlcClient : IPlcClient
{
    public abstract PlcVendor Vendor { get; }

    protected abstract IReadOnlyList<string> DemoTags { get; }

    public Task<PlcReadResult> TestReadAsync(PlcConfig config, CancellationToken cancellationToken)
    {
        return ReadAsync(config, cancellationToken, "TEST");
    }

    public Task<PlcReadResult> ReadProductionAsync(PlcConfig config, CancellationToken cancellationToken)
    {
        return ReadAsync(config, cancellationToken, "RUN");
    }

    private async Task<PlcReadResult> ReadAsync(PlcConfig config, CancellationToken cancellationToken, string mode)
    {
        var connected = await CanReachEndpointAsync(config.IpAddress, config.Port, cancellationToken);
        if (!connected)
        {
            return new PlcReadResult
            {
                Vendor = Vendor.ToString(),
                IpAddress = config.IpAddress,
                Success = false,
                Message = $"{mode}: Cannot connect to {config.IpAddress}:{config.Port}",
                Values = []
            };
        }

        await Task.Delay(60, cancellationToken);

        var values = DemoTags.ToDictionary(
            keySelector: tag => tag,
            elementSelector: _ => (object)Random.Shared.Next(0, 1000));

        return new PlcReadResult
        {
            Vendor = Vendor.ToString(),
            IpAddress = config.IpAddress,
            Success = true,
            Message = $"{mode}: Read successful",
            Values = values
        };
    }

    private static async Task<bool> CanReachEndpointAsync(string ipAddress, int port, CancellationToken cancellationToken)
    {
        try
        {
            using var tcpClient = new TcpClient();
            using var timeoutTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            using var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutTokenSource.Token);

            await tcpClient.ConnectAsync(ipAddress, port, linkedTokenSource.Token);
            return tcpClient.Connected;
        }
        catch
        {
            return false;
        }
    }
}
