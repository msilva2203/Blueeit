namespace Blueeit.Api.Models;

public class Forum
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public int AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string Title { get; set; }
}