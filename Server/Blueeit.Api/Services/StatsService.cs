using Blueeit.Api.DTOs.Stats;

namespace Blueeit.Api.Services;

public interface IStatsService
{
    Task<StatsResponse> GetStatsAsync();
}

public class InMemoryStatsService : IStatsService
{
    private readonly IUserService _userService;
    private readonly IForumService _forumService;
    private readonly IForumThreadService _threadService;
    private readonly IPostService _postService;

    public InMemoryStatsService(
        IUserService userService,
        IForumService forumService,
        IForumThreadService threadService,
        IPostService postService)
    {
        _userService = userService;
        _forumService = forumService;
        _threadService = threadService;
        _postService = postService;
    }

    public async Task<StatsResponse> GetStatsAsync()
    {
        var userCount = await _userService.GetCountAsync();
        var forumCount = await _forumService.GetCountAsync();
        var threadCount = await _threadService.GetCountAsync();
        var postCount = await _postService.GetCountAsync();

        var result = new StatsResponse
        {
            UserCount = userCount,
            ForumCount = forumCount,
            ThreadCount = threadCount,
            PostCount = postCount
        };

        return result;
    }
}