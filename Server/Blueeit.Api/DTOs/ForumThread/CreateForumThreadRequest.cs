namespace Blueeit.Api.DTOs.ForumThread;

public class CreateForumThreadRequest
{
    public int AuthorId { get; set; }
    public int? ForumId { get; set; }
    public required string Title { get; set; }
}
