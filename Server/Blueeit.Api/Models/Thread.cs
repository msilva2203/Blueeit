namespace Blueeit.Api.Models;

public class Thread
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public required string Title { get; set; }
    public DateTime StartDate { get; set; }
}
