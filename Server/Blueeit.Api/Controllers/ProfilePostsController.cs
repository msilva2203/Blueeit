using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/profile-posts")]
public class ProfilePostsController : ControllerBase
{
    private readonly IProfilePostService _profilePostService;

    public ProfilePostsController(IProfilePostService profilePostService)
    {
        _profilePostService = profilePostService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ProfilePostResponse>>> GetAllProfilePosts(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _profilePostService.GetProfilePostsAsync(page, pageSize);

        var responses = result.Items
            .Select(profilePost => profilePost.ToResponse())
            .ToList();

        return Ok(new PaginatedResult<ProfilePostResponse>
        {
            Items = responses,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProfilePostResponse>> GetProfilePost(
        [FromRoute] int id)
    {
        ProfilePost? result = await _profilePostService.GetProfilePostByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        var response = result.ToResponse();

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProfilePost(
        [FromRoute] int id)
    {
        var result = await _profilePostService.DeleteProfilePostAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}