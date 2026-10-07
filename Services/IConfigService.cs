using mes_plc.Models;

namespace mes_plc.Services;

public interface IConfigService
{
    Task<PlcConfig> LoadAsync(CancellationToken cancellationToken);

    Task SaveAsync(PlcConfig config, CancellationToken cancellationToken);
}
