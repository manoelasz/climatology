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

    public async Task<WeatherRecord> BuscarESalvarClima(string cidade)
    {
        var apiResponse = await _client.GetWeatherAsync(cidade);

        if (apiResponse == null)
            throw new Exception("Não foi possível obter dados climáticos");

        if (string.IsNullOrWhiteSpace(cidade))
            throw new ArgumentException("Cidade é obrigatória", nameof(cidade));

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

        return record;
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
            return new EstatisticasDto
            {
                Minima = 0,
                Maxima = 0,
                Media = 0,
                Registros = 0
            };

        return new EstatisticasDto
        {
            Minima = data.Min(x => x.TemperaturaMin),
            Maxima = data.Max(x => x.TemperaturaMax),
            Media = data.Average(x => x.Temperatura),
            Registros = data.Count
        };
    }
}
