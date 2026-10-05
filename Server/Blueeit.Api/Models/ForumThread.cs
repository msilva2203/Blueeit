namespace Blueeit.Api.Models;

public class ForumThread
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int? ForumId { get; set; }
    public int? OpeningPostId { get; set; }
    public DateTime CreationDate { get; set; }
    public required string Title { get; set; }
}
