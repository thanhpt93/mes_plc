using mes_plc.Models;

namespace mes_plc.Services;

public interface IPlcClientFactory
{
    IPlcClient Create(PlcVendor vendor);
}
