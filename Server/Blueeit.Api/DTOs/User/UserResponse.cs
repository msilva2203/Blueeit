namespace Blueeit.Api.DTOs.User;

public class UserResponse
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public DateTime CreationDate { get; set; }
    public UserMetadata Metadata { get; set; } = new();
}
