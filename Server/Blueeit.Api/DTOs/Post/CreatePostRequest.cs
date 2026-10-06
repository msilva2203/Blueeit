namespace Blueeit.Api.DTOs.Post;

public class CreatePostRequest
{
    public int AuthorId { get; set; }
    public required string Content { get; set; }
}
