using Blueeit.Api.DTOs.User;

namespace Blueeit.Api.DTOs.ProfilePost;

public class ProfilePostResponse
{
    public int Id { get; set; }
    public required UserSummary User { get; set; }
    public required UserSummary Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Content { get; set; }
}