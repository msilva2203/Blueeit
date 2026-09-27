using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.Services;
using Blueeit.Api.Mappings;
using Microsoft.AspNetCore.Mvc;
using Blueeit.Api.DTOs.ForumThread;

namespace Blueeit.Api.Controllers;

[ApiController]
[Route("api/v1/forums")]
public class ForumController : ControllerBase
{
    private IForumService _forumService;
    private IForumThreadService _threadService;

    public ForumController(IForumService forumService, IForumThreadService threadService)
    {
        _forumService = forumService;
        _threadService = threadService;
    }

    /// <summary>
    /// Gets all the forums.
    /// </summary>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The maximum number of forums to retrieve per page.</param>
    /// <returns>A paginated collection of forums.</returns>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ForumResponse>>> GetAllForums(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _forumService.GetRootForumsAsync(page, pageSize);

        var responses = result.Items
            .Select(forum => forum.ToResponse())
            .ToList();

        return Ok(new PaginatedResult<ForumResponse>
        {
            Items = responses,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        });
    }

    /// <summary>
    /// Gets the subforums belonging to a forum.
    /// </summary>
    /// <param name="id">The ID of the parent forum.</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The maximum number of subforums to retrieve per page.</param>
    /// <returns>A paginated collection of subforums.</returns>
    [HttpGet("{id:int}/subforums")]
    public async Task<ActionResult<PaginatedResult<ForumResponse>>> GetSubforums(
        [FromRoute] int id,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _forumService.GetSubforumsAsync(id, page, pageSize);

        var responses = result.Items
            .Select(forum => forum.ToResponse())
            .ToList();

        return Ok(new PaginatedResult<ForumResponse>
        {
            Items = responses,
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        });
    }

    [HttpGet("{id:int}/threads")]
    public async Task<ActionResult<PaginatedResult<ForumThreadResponse>>> GetThreads(
        [FromRoute] int id,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _threadService.GetThreadsByForumIdAsync(id, page, pageSize);

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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ForumResponse>> GetForum(
        [FromRoute] int id)
    {
        var forum = await _forumService.GetForumByIdAsync(id);

        if (forum is null)
        {
            return NotFound();
        }

        var response = forum.ToResponse();

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ForumResponse>> CreateForum(
        [FromBody] CreateForumRequest request)
    {
        var forum = await _forumService.CreateForumAsync
        (
            request.ParentId,
            request.AuthorId, 
            request.Title
        );

        var response = forum.ToResponse();

        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteForum(
        [FromRoute] int id)
    {
        var result = await _forumService.DeleteForumAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}