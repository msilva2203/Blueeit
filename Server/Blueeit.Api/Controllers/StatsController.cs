using Blueeit.Api.DTOs.Stats;
using Blueeit.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blueeit.Api.Controllers;

[ApiController]
[Route("api/v1/stats")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;

    public StatsController(IStatsService statsService)
    {
        _statsService = statsService;
    }

    [HttpGet]
    public async Task<ActionResult<StatsResponse>> GetStats()
    {
        var result = await _statsService.GetStatsAsync();

        return Ok(result);
    }
}