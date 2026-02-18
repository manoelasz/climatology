using Microsoft.AspNetCore.Mvc;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _service;

    public WeatherController(IWeatherService service)
    {
        _service = service;
    }

    [HttpPost("consultar")]
    public async Task<IActionResult> Consultar([FromQuery] string cidade)
    {
        var result = await _service.BuscarESalvarClima(cidade);
        return Ok(result);
    }

    [HttpGet("periodo")]
    public async Task<IActionResult> GetByPeriod([FromQuery] string cidade, DateTime inicio, DateTime fim)
    {
        var result = await _service.BuscarPorPeriodo(cidade, inicio, fim);
        return Ok(result);
    }

    [HttpGet("estatisticas-hoje")]
    public async Task<IActionResult> GetTodayStats([FromQuery] string cidade)
    {
        var stats = await _service.ObterEstatisticasHoje(cidade);
        return Ok(stats);
    }
}
