using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.Post;
using Blueeit.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blueeit.Api.Controllers;

[ApiController]
[Route("api/v1/threads")]
public class ForumThreadController : ControllerBase
{
    private IForumThreadService _threadService;
    private IPostService _postService;

    public ForumThreadController(
        IForumThreadService threadService,
        IPostService postService)
    {
        _threadService = threadService;
        _postService = postService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ForumThreadResponse>>> GetAllThreads(
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20)
    {
        var result = await _threadService.GetAllThreadsAsync(page, pageSize);

        return Ok(result);
    }

    [HttpGet("{id:int}/posts")]
    public async Task<ActionResult<PostResponse>> GetPosts(
        [FromRoute] int id,
        [FromQuery] PostQueryKey key = PostQueryKey.None,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _postService.GetPostsByThreadId(
            key,
            id,
            page,
            pageSize
        );

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ForumThreadResponse>> GetThread(
        [FromRoute] int id)
    {
        var result = await _threadService.GetThreadByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("{id:int}/posts")]
    public async Task<ActionResult<PostResponse>> CreatePost(
        [FromRoute] int id,
        [FromBody] CreatePostRequest request)
    {
        var result = await _postService.CreatePostAsync(
            id,
            request.AuthorId,
            request.Content
        );

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteThread(
        [FromRoute] int id)
    {
        var result = await _threadService.DeleteThreadAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
