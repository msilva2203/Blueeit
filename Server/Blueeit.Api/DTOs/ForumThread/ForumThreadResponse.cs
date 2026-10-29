using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.DTOs.User;

namespace Blueeit.Api.DTOs.ForumThread;

public class ForumThreadResponse
{
    public int Id { get; set; }
    public required UserSummary Author { get; set; }
    public required ForumSummary Forum { get; set; }
    public required PostSummary OpeningPost { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Title { get; set; }
}
