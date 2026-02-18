using System.Text.Json.Serialization;

namespace backend.Models;

public class OpenWeatherResponse
{
    [JsonPropertyName("main")]
    public MainData Main { get; set; } = new();
}

public class MainData
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("temp_min")]
    public double TempMin { get; set; }

    [JsonPropertyName("temp_max")]
    public double TempMax { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
}
