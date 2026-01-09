using Microsoft.AspNetCore.Mvc;
using StockDashboard.Api.Services;

namespace StockDashboard.Api.Controllers;

[ApiController]
[Route("api")]
public class StockDataController : ControllerBase
{
    private readonly StockQueryService _service;

    public StockDataController(StockQueryService service)
    {
        _service = service;
    }

    [HttpGet("data/{symbol}")]
    public async Task<IActionResult> GetData(string symbol)
    {
        var data = await _service.GetLast30DaysAsync(symbol.ToUpperInvariant());
        if (!data.Any()) return NotFound($"No data found for symbol {symbol}");
        return Ok(data);
    }

    [HttpGet("summary/{symbol}")]
    public async Task<IActionResult> GetSummary(string symbol)
    {
        var summary = await _service.GetSummaryAsync(symbol.ToUpperInvariant());
        if (summary is null) return NotFound($"No summary found for symbol {symbol}");
        return Ok(summary);
    }

    [HttpGet("compare")]
    public async Task<IActionResult> Compare([FromQuery] string symbol1, [FromQuery] string symbol2)
    {
        if (string.IsNullOrWhiteSpace(symbol1) || string.IsNullOrWhiteSpace(symbol2))
            return BadRequest("Both symbol1 and symbol2 are required.");

        var result = await _service.CompareAsync(
            symbol1.ToUpperInvariant(),
            symbol2.ToUpperInvariant());

        if (result is null) return NotFound("Comparison data not available.");
        return Ok(result);
    }
}
