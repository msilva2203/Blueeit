namespace Blueeit.Api.Models;

public class Post
{
    public int Id { get; set; }
    public int ThreadId { get; set; }
    public int OwnerId { get; set; }
    public DateTime Date { get; set; }
}
