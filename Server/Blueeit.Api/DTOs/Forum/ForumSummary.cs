namespace Blueeit.Api.DTOs.Forum;

public class ForumSummary
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Title { get; set; }
}