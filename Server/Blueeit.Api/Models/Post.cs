namespace Blueeit.Api.Models;

public class Post
{
    public int Id { get; set; }
    public int ThreadId { get; set; }
    public int AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Content { get; set; }
}
