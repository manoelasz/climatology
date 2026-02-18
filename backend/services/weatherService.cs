using backend.Models;
using backend.Repository;

namespace backend.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _repository;
    private readonly OpenWeatherClient _client;

    public WeatherService(
        IWeatherRepository repository,
        OpenWeatherClient client)
    {
        _repository = repository;
        _client = client;
    }

    public async Task BuscarESalvarClima(string cidade)
    {
        var apiResponse = await _client.GetWeatherAsync(cidade);

        if (apiResponse == null)
            throw new Exception("Não foi possível obter dados climáticos");

        var record = new WeatherRecord
        {
            Cidade = cidade,
            DataConsulta = DateTime.UtcNow,
            Temperatura = apiResponse.Main.Temp,
            TemperaturaMin = apiResponse.Main.TempMin,
            TemperaturaMax = apiResponse.Main.TempMax,
            Umidade = apiResponse.Main.Humidity
        };

        await _repository.AddAsync(record);
    }

    public async Task<List<WeatherRecord>> BuscarPorPeriodo(
        string cidade,
        DateTime inicio,
        DateTime fim)
    {
        return await _repository.GetByPeriodAsync(cidade, inicio, fim);
    }

    public async Task<object?> ObterEstatisticasHoje(string cidade)
    {
        var data = await _repository.GetTodayAsync(cidade);

        if (!data.Any())
            return null;

        return new
        {
            Minima = data.Min(x => x.TemperaturaMin),
            Maxima = data.Max(x => x.TemperaturaMax),
            Media = data.Average(x => x.Temperatura),
            Registros = data.Count
        };
    }
}
