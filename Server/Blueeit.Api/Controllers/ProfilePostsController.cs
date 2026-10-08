using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.ProfilePost;
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

    /// <summary>
    /// Gets all profile posts.
    /// </summary>
    /// <param name="key">The query key to use when sorting the data.</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The maximum number of profile posts to retrieve per page.</param>
    /// <returns>A paginated collection of profile posts.</returns>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ProfilePostResponse>>> GetAllProfilePosts(
        [FromQuery] ProfilePostQueryKey key = ProfilePostQueryKey.None,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _profilePostService.GetProfilePostsAsync(
            key,
            page, 
            pageSize);

        return Ok(result);
    }

    /// <summary>
    /// Gets the profile post with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the profile post to retrieve.</param>
    /// <returns>A profile post.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProfilePostResponse>> GetProfilePost(
        [FromRoute] int id)
    {
        var result = await _profilePostService.GetProfilePostByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes a profile post.
    /// </summary>
    /// <param name="id">The ID of the profile post to delete.</param>
    /// <returns>The result of the operation.</returns>
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