using MadaDaPesca.Application.Services;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MadaDaPesca.Application.Adapters;

public static class JsonSerializerAdapters
{
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    public static JsonSerializerOptions Options => _options;

    public static string Serialize<T>(T body)
    {
        return JsonSerializer.Serialize(body, _options);
    }

    public static T? FromJson<T>(Stream body)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(body, _options);
        }
        catch (Exception ex)
        {
            LogService.LogApi("Erro desserealização JSON", ex);
            return default;
        }
    }

    public static StringContent ToJson<T>(T body)
    {
        var json = JsonSerializer.Serialize(body, _options);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
