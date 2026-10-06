namespace Blueeit.Api.Models;

public enum ActivityType
{
    ForumCreated,
    ThreadCreated,
    PostCreated,
    ProfilePostCreated
}

public class Activity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ActivityType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? ForumId { get; set; }
    public int? ThreadId { get; set; }
    public int? PostId { get; set; }
    public int? ProfilePostId { get; set; }
    public int? ProfileUserId { get; set; }
}