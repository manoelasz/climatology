namespace backend.Models;

public class WeatherRecord
{
    public int Id { get; set; }
    public string Cidade { get; set; } = string.Empty;
    public DateTime DataConsulta { get; set; }

    public double Temperatura { get; set; }
    public double TemperaturaMin { get; set; }
    public double TemperaturaMax { get; set; }

    public int Umidade { get; set; }
}

public class OpenWeatherSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string Units { get; set; } = "metric";
}