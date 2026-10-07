using Microsoft.Win32;

namespace mes_plc.Services;

public sealed class StartupRegistrationService : IStartupRegistrationService
{
    private const string RunRegistryPath = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
    private const string ApplicationName = "MesPlcCollector";

    public bool IsRegistered()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunRegistryPath, writable: false);
        var value = key?.GetValue(ApplicationName) as string;
        return !string.IsNullOrWhiteSpace(value);
    }

    public void SetRegistration(bool enabled)
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunRegistryPath, writable: true)
                       ?? Registry.CurrentUser.CreateSubKey(RunRegistryPath, writable: true);

        if (enabled)
        {
            var exePath = Environment.ProcessPath;
            if (string.IsNullOrWhiteSpace(exePath))
            {
                return;
            }

            key?.SetValue(ApplicationName, $"\"{exePath}\"");
            return;
        }

        key?.DeleteValue(ApplicationName, throwOnMissingValue: false);
    }
}
