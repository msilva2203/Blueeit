using Blueeit.Api.Models;
using Blueeit.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blueeit.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id:int}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _userService.GetUserByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("")]
    public ActionResult<IReadOnlyList<User>> GetAllUsers()
    {
        var users = _userService.GetAllUsers();

        return Ok(users);
    }

    [HttpPost("")]
    public ActionResult<User> CreateUser(UserCreationDto userCreationDto)
    {
        var username = userCreationDto.Username;
        var email = userCreationDto.Email;

        var user = _userService.CreateUserAsync(username, email);

        return Ok(user);
    }
}
