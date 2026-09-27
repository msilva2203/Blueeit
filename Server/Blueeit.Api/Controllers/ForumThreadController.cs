using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blueeit.Api.Controllers;

[ApiController]
[Route("api/v1/threads")]
public class ForumThreadController : ControllerBase
{
    private IForumThreadService _threadService;
    private IPostService _postService;

    public ForumThreadController(IForumThreadService threadService, IPostService postService)
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

        var responses = result.Items
            .Select(thread => thread.ToResponse())
            .ToList();

        return Ok(new PaginatedResult<ForumThreadResponse>
        {
            Items = responses,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        });
    }

    [HttpGet("{id:int}/posts")]
    public async Task<ActionResult<PostResponse>> GetPosts(
        [FromRoute] int id,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _postService.GetPostsByThreadId(id, page, pageSize);

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
    public async Task<ActionResult<ForumThreadResponse>> GetThread(
        [FromRoute] int id)
    {
        var thread = await _threadService.GetThreadByIdAsync(id);

        if (thread is null)
        {
            return NotFound();
        }

        var response = thread.ToResponse();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ForumThreadResponse>> CreateThread(
        [FromBody] CreateForumThreadRequest request)
    {
        var thread = await _threadService.CreateThreadAsync
        (
            request.AuthorId,
            request.ForumId,
            request.Title
        );

        var response = thread.ToResponse();

        return Ok(response);
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
