using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Blueeit.Api.DTOs.UserActivity;

public class UserActivityResponse
{
    public int Id { get; set; }
    public required UserSummary User { get; set; }
    public UserActivityType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public required ForumSummary? Forum { get; set; }
    public required ForumThreadSummary? Thread { get; set; }
    public required PostResponse? Post { get; set; }
    public required ProfilePostResponse? ProfilePost { get; set; }
    public required UserSummary? ProfileOwner { get; set; }
}