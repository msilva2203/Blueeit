using Blueeit.Api.Common.Pagination;
using Blueeit.Api.Services;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Mappings;
using Microsoft.AspNetCore.Mvc;
using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Queries.User;

namespace Blueeit.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IProfilePostService _profilePostService;

    public UserController(
        IUserService userService, 
        IProfilePostService profilePostService)
    {
        _userService = userService;
        _profilePostService = profilePostService;
    }

    /// <summary>
    /// Gets all the users.
    /// </summary>
    /// <param name="key">The sort key used to order the users by.</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The maximum number of users to retrieve per page.</param>
    /// <returns>A paginated collection of users.</returns>
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<UserResponse>>> GetAllUsers(
        [FromQuery] UserQueryKey key = UserQueryKey.None,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20)
    {
        var result = await _userService.GetAllUsersAsync(key, page, pageSize);

        return Ok(result);
    }

    /// <summary>
    /// Gets the user with a specific ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The user.</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse>> GetUser(
        [FromRoute] int id)
    {
        var result = await _userService.GetUserByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">The request body.</param>
    /// <returns>The newly created user.</returns>
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUser(
        [FromBody] CreateUserRequest request)
    {
        var result = await _userService.CreateUserAsync(
            request.Username,
            request.Email,
            request.Password
        );

        return Ok(result);
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The result of the operation.</returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(
        [FromRoute] int id)
    {
        var result = await _userService.DeleteUserAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Gets all the profile posts for a specific user.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="page">The page number to retrieve.</param>
    /// <param name="pageSize">The maximum number of users to retrieve per page.</param>
    /// <returns>A paginated collection of profile posts.</returns>
    [HttpGet("{id:int}/profile-posts")]
    public async Task<ActionResult<PaginatedResult<ProfilePostResponse>>> GetAllProfilePosts(
        [FromRoute] int id,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _profilePostService.GetProfilePostsByUserIdAsync(id, page, pageSize);

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

    /// <summary>
    /// Creates a new profile post for a specific user.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="request">The request body.</param>
    /// <returns>The newly created profile post.</returns>
    [HttpPost("{id:int}/profile-posts")]
    public async Task<ActionResult<ProfilePostResponse>> CreateProfilePost(
        [FromRoute] int id,
        [FromBody] CreateProfilePostRequest request)
    {
        var result = await _profilePostService.CreateProfilePostAsync(
            id,
            request.AuthorId,
            request.Content
        );

        var response = result.ToResponse();

        return Ok(response);
    }
}
