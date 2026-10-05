namespace Blueeit.Api.DTOs.ProfilePost;

public class ProfilePostResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AuthorId { get; set; }
    public DateTime CreationDate { get; set; }
    public required string Content { get; set; }
}