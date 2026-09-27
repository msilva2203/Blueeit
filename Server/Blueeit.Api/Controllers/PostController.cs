using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Mappings;
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
    public async Task<ActionResult<IReadOnlyList<PostResponse>>> GetAllPosts([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _postService.GetAllPostsAsync(page, pageSize);

        var responses = result.Items
            .Select(post => post.ToResponse())
            .ToList();

        return Ok(new PaginatedResult<PostResponse>
        {
            Items = responses,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostResponse>> GetPostById(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);

        if (post is null)
        {
            return NotFound();
        }

        var response = post.ToResponse();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<PostResponse>> CreatePost(CreatePostRequest request)
    {
        var post = await _postService.CreatePostAsync
        (
            request.ThreadId,
            request.AuthorId,
            request.Content
        );

        var response = post.ToResponse();

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        bool result = await _postService.DeletePostAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
