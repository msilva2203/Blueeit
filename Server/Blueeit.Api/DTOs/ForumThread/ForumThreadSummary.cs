namespace Blueeit.Api.DTOs.ForumThread;

public class ForumThreadSummary
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Title { get; set; }
}