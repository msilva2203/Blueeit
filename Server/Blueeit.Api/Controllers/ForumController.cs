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

        return Ok(result);
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

        return Ok(result);
    }

    /// <summary>
    /// Gets the threads belonging to a forum.
    /// </summary>
    /// <param name="id">The ID of the forum.</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The maximum number of threads to retrieve per page.</param>
    /// <returns>A paginated collection of threads.</returns>
    [HttpGet("{id:int}/threads")]
    public async Task<ActionResult<PaginatedResult<ForumThreadResponse>>> GetThreads(
        [FromRoute] int id,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _threadService.GetThreadsByForumIdAsync(id, page, pageSize);
        
        return Ok(result);
    }

    /// <summary>
    /// Gets the forum with a specific ID.
    /// </summary>
    /// <param name="id">The ID of the forum.</param>
    /// <returns>The forum.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ForumResponse>> GetForum(
        [FromRoute] int id)
    {
        var forum = await _forumService.GetForumByIdAsync(id);

        if (forum is null)
        {
            return NotFound();
        }

        return Ok(forum);
    }

    /// <summary>
    /// Creates a new forum.
    /// </summary>
    /// <param name="request">The request body.</param>
    /// <returns>The newly created forum.</returns>
    [HttpPost]
    public async Task<ActionResult<ForumResponse>> CreateForum(
        [FromBody] CreateForumRequest request)
    {
        var forum = await _forumService.CreateForumAsync(
            request.ParentId,
            request.AuthorId, 
            request.Title
        );

        return Ok(forum);
    }

    /// <summary>
    /// Deletes a forum.
    /// </summary>
    /// <param name="id">The ID of the forum.</param>
    /// <returns>The result of the operation.</returns>
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