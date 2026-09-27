namespace Blueeit.Api.DTOs.User;

public class CreateUserRequest
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
