using Blueeit.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Blueeit.Api.DTOs.UserActivity;

public class UserActivityResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserActivityType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public int? ForumId { get; set; }
    public int? ThreadId { get; set; }
    public int? PostId { get; set; }
    public int? ProfilePostId { get; set; }
    public int? ProfileOwnerId { get; set; }
}