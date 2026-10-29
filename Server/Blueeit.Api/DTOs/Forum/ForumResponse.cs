using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.User;

namespace Blueeit.Api.DTOs.Forum;

public class ForumResponse
{
    public int Id { get; set; }
    public required UserSummary Author { get; set; }
    public required ForumSummary? Parent { get; set; }
    public required ForumThreadSummary? LatestThread { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Title { get; set; }
    public ForumMetadata Metadata { get; set; } = new();
}