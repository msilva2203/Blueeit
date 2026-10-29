using Blueeit.Api.DTOs.Stats;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Mappings;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public class InMemoryStatsService : IStatsService
{
    private readonly IUserRepository _userRepository;
    private readonly IForumRepository _forumRepository;
    private readonly IForumThreadRepository _threadRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserFactoryService _userFactory;

    public InMemoryStatsService(
        IUserRepository userRepository,
        IForumRepository forumRepository,
        IForumThreadRepository threadRepository,
        IPostRepository postRepository,
        IUserFactoryService userFactory)
    {
        _userRepository = userRepository;
        _forumRepository = forumRepository;
        _threadRepository = threadRepository;
        _postRepository = postRepository;
        _userFactory = userFactory;
    }

    public async Task<StatsResponse> GetStatsAsync()
    {
        var userCount = await _userRepository.GetCountAsync();
        var forumCount = await _forumRepository.GetCountAsync();
        var threadCount = await _threadRepository.GetCountAsync();
        var postCount = await _postRepository.GetCountAsync();
        var latestUser = await _userRepository.GetLatestAsync();

        var result = new StatsResponse
        {
            UserCount = userCount,
            ForumCount = forumCount,
            ThreadCount = threadCount,
            PostCount = postCount,
            LatestUser = latestUser is not null ? _userFactory.MakeSummary(latestUser) : null
        };

        return result;
    }
}