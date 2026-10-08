using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.User;

namespace Blueeit.Api.DTOs.Post;

public class PostResponse
{
    public int Id { get; set; }
    public required ForumThreadSummary Thread { get; set; }
    public required UserSummary Author { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Content { get; set; }
}