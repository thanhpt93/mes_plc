namespace mes_plc.Services;

public interface IStartupRegistrationService
{
    bool IsRegistered();

    void SetRegistration(bool enabled);
}
