using System.Text.Json;
using mes_plc.Models;

namespace mes_plc.Services;

public sealed class ConfigService : IConfigService
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    private readonly string _configPath = Path.Combine(AppContext.BaseDirectory, "config.json");

    public async Task<PlcConfig> LoadAsync(CancellationToken cancellationToken)
    {
        if (!File.Exists(_configPath))
        {
            return new PlcConfig();
        }

        await using var stream = File.OpenRead(_configPath);
        var config = await JsonSerializer.DeserializeAsync<PlcConfig>(stream, JsonSerializerOptions, cancellationToken);
        return config ?? new PlcConfig();
    }

    public async Task SaveAsync(PlcConfig config, CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_configPath)!);
        await using var stream = File.Create(_configPath);
        await JsonSerializer.SerializeAsync(stream, config, JsonSerializerOptions, cancellationToken);
    }
}
