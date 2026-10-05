namespace Blueeit.Api.Models;

public class ProfilePost
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AuthorId { get; set; }
    public DateTime CreationDate { get; set; }
    public required string Content { get; set; }
}