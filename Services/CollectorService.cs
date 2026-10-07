using mes_plc.Models;

namespace mes_plc.Services;

public sealed class CollectorService(IPlcClientFactory plcClientFactory) : ICollectorService
{
    public async Task StartAsync(PlcConfig config, Action<PlcReadResult> onRead, Action<Exception> onError, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(onRead);
        ArgumentNullException.ThrowIfNull(onError);

        var client = plcClientFactory.Create(config.Vendor);
        var delayMs = Math.Max(200, config.PollingIntervalMs);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var result = await client.ReadProductionAsync(config, cancellationToken);
                onRead(result);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                onError(ex);
            }

            try
            {
                await Task.Delay(delayMs, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }
}
