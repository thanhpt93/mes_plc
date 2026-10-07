using mes_plc.Models;

namespace mes_plc.Services;

public interface ICollectorService
{
    Task StartAsync(PlcConfig config, Action<PlcReadResult> onRead, Action<Exception> onError, CancellationToken cancellationToken);
}
