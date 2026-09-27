namespace Blueeit.Api.DTOs.Forum;

public class ForumResponse
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public int AuthorId { get; set; }
    public DateTime CreationDate { get; set; }
    public required string Title { get; set; }
}