using Blueeit.Api.DTOs.User;

namespace Blueeit.Api.DTOs.Stats;

public class StatsResponse
{
    public int UserCount { get; set; }
    public int ForumCount { get; set; }
    public int ThreadCount { get; set; }
    public int PostCount { get; set; }
    public UserResponse? LatestUser { get; set; }
}