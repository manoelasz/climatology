using backend.Services;

namespace backend.BackgroundServices;

public class WeatherBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly List<string> _capitais = new()
    {
        "São Paulo", "Rio de Janeiro", "Brasília"
    };

    public WeatherBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IWeatherService>();

            foreach (var cidade in _capitais)
            {
                await service.BuscarESalvarClima(cidade);
            }

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}
