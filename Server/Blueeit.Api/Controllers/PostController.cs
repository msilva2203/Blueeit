using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Mappings;
using Blueeit.Api.Queries.Post;
using Blueeit.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blueeit.Api.Controllers;

[ApiController]
[Route("api/v1/posts")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<PostResponse>>> GetAllPosts(
        [FromQuery] PostQueryKey key = PostQueryKey.None,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20)
    {
        var result = await _postService.GetAllPostsAsync(
            key,
            page,
            pageSize
        );

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostResponse>> GetPostById(
        [FromRoute] int id)
    {
        var result = await _postService.GetPostByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(
        [FromRoute] int id)
    {
        bool result = await _postService.DeletePostAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
