using mes_plc.Models;

namespace mes_plc.Services;

public sealed class PlcClientFactory : IPlcClientFactory
{
    public IPlcClient Create(PlcVendor vendor)
    {
        return vendor switch
        {
            PlcVendor.Siemens => new SiemensPlcClient(),
            PlcVendor.Mitsubishi => new MitsubishiPlcClient(),
            PlcVendor.Omron => new OmronPlcClient(),
            _ => throw new ArgumentOutOfRangeException(nameof(vendor), vendor, null)
        };
    }
}
