using backend.Models;

namespace backend.Repository;

public interface IWeatherRepository
{
    Task AddAsync(WeatherRecord record);
    Task<List<WeatherRecord>> GetByPeriodAsync(string cidade, DateTime inicio, DateTime fim);
    Task<List<WeatherRecord>> GetTodayAsync(string cidade);
}
