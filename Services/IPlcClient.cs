using mes_plc.Models;

namespace mes_plc.Services;

public interface IPlcClient
{
    PlcVendor Vendor { get; }

    Task<PlcReadResult> TestReadAsync(PlcConfig config, CancellationToken cancellationToken);

    Task<PlcReadResult> ReadProductionAsync(PlcConfig config, CancellationToken cancellationToken);
}
