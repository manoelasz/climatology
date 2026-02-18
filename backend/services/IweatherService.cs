using backend.Models;

namespace backend.Services;

public interface IWeatherService
{
    Task<WeatherRecord> BuscarESalvarClima(string cidade);
    Task<List<WeatherRecord>> BuscarPorPeriodo(string cidade, DateTime inicio, DateTime fim);
    Task<EstatisticasDto?> ObterEstatisticasHoje(string cidade);
}
