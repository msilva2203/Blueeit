namespace Blueeit.Api.DTOs.User;

public class UserSummary
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}