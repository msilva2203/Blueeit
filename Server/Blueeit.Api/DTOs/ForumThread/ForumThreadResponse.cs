namespace Blueeit.Api.DTOs.ForumThread;

public class ForumThreadResponse
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int? ForumId { get; set; }
    public DateTime CreationDate { get; set; }
    public required string Title { get; set; }
}
