using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.UserActivity;
using Blueeit.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blueeit.Api.Controllers;

[ApiController]
[Route("api/v1/activity")]
public class UserActivityController : ControllerBase
{
    private readonly IUserActivityService _activityService;

    public UserActivityController(IUserActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<UserActivityResponse>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _activityService.GetAllAsync(
            page, 
            pageSize);

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserActivityResponse>> Get(
        [FromRoute] int id)
    {
        var result = await _activityService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _activityService.DeleteByIdAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}