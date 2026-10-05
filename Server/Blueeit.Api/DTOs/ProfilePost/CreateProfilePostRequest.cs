namespace Blueeit.Api.DTOs.ProfilePost;

public class CreateProfilePostRequest
{
    public int AuthorId { get; set; }
    public required string Content { get; set; }
}