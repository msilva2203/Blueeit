namespace Blueeit.Api.DTOs.Forum;

public class CreateForumRequest
{
    public int AuthorId { get; set; }
    public required string Title { get; set; }
}