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

    [HttpGet("consultar")]
    public async Task<IActionResult> Consultar([FromQuery] string cidade)
    {
        try
        {
            var registro = await _service.BuscarESalvarClima(cidade);
            return Ok(registro);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet("periodo")]
    public async Task<IActionResult> GetByPeriod([FromQuery] string cidade, DateTime inicio, DateTime fim)
    {
        try
        {
            var result = await _service.BuscarPorPeriodo(cidade, inicio, fim);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    [HttpGet("estatisticas-hoje")]
    public async Task<IActionResult> GetTodayStats([FromQuery] string cidade)
    {
        try
        {
            var stats = await _service.ObterEstatisticasHoje(cidade);
            return Ok(stats);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}
