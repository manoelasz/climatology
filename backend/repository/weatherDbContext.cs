using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository;

public class WeatherRepository : IWeatherRepository
{
    private readonly AppDbContext _context;

    public WeatherRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(WeatherRecord record)
    {
        await _context.WeatherRecords.AddAsync(record);
        await _context.SaveChangesAsync();
    }

    public async Task<List<WeatherRecord>> GetByPeriodAsync(string cidade, DateTime inicio, DateTime fim)
    {
        inicio = inicio.Date;
        fim = fim.Date.AddDays(1).AddTicks(-1);

        return await _context.WeatherRecords
            .Where(x =>
                x.Cidade == cidade &&
                x.DataConsulta >= inicio &&
                x.DataConsulta <= fim)
            .ToListAsync();
    }

    public async Task<List<WeatherRecord>> GetTodayAsync(string cidade)
    {
        var hoje = DateTime.UtcNow.Date;

        return await _context.WeatherRecords
            .Where(x => x.Cidade == cidade &&
                        x.DataConsulta.Date == hoje)
            .ToListAsync();
    }
}
