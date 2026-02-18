using backend.Models;

namespace backend.Services;

public interface IWeatherService
{
    Task BuscarESalvarClima(string cidade);
    Task<List<WeatherRecord>> BuscarPorPeriodo(string cidade, DateTime inicio, DateTime fim);
    Task<object?> ObterEstatisticasHoje(string cidade);
}
