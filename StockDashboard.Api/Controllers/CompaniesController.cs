using Microsoft.AspNetCore.Mvc;
using StockDashboard.Api.Services;

namespace StockDashboard.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly StockQueryService _service;

    public CompaniesController(StockQueryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _service.GetCompaniesAsync();
        return Ok(companies);
    }
}
