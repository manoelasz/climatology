using System.Text.Json;
using backend.Models;
using Microsoft.Extensions.Options;

namespace backend.Services;

public class OpenWeatherClient
{
    private readonly HttpClient _httpClient;
    private readonly OpenWeatherSettings _settings;

    public OpenWeatherClient(
        HttpClient httpClient,
        IOptions<OpenWeatherSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings.Value;
    }

    public async Task<OpenWeatherResponse?> GetWeatherAsync(string cidade)
    {
        var url = $"{_settings.BaseUrl}weather?q={cidade}&appid={_settings.ApiKey}&units={_settings.Units}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Erro ao consultar OpenWeather");

        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<OpenWeatherResponse>(content);
    }
}
