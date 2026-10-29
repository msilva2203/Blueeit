using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public interface IForumFactoryService
{
    ForumMetadata MakeMetadata(Forum forum);

    ForumResponse MakeResponse(Forum forum);

    ForumSummary MakeSummary(Forum forum);
}

public class ForumFactoryService : IForumFactoryService
{
    private readonly IUserRepository _userRepository;
    private readonly IForumRepository _forumRepository;
    private readonly IForumThreadRepository _threadRepository;

    public ForumFactoryService(
        IUserRepository userRepository,
        IForumRepository forumRepository,
        IForumThreadRepository threadRepository)
    {
        _userRepository = userRepository;
        _forumRepository = forumRepository;
        _threadRepository = threadRepository;
    }

    public ForumMetadata MakeMetadata(Forum forum)
    {
        return new ForumMetadata
        {
            ThreadCount = _threadRepository.GetCountByForumId(forum.Id),
            PostCount = 0
        };
    }

    public ForumResponse MakeResponse(Forum forum)
    {
        User? author = _userRepository.GetById(forum.AuthorId);

        Forum? parent = null;
        if (forum.ParentId is not null)
        {
            parent = _forumRepository.GetById(forum.ParentId.Value);
        }

        ForumThread? latestThread = _threadRepository.GetLatestByForumId(forum.Id);

        return forum.ToResponse(
            author.ToSummary(),
            parent is not null ? parent.ToSummary() : null,
            latestThread is not null ? latestThread.ToSummary() : null,
            MakeMetadata(forum)
        );
    }

    public ForumSummary MakeSummary(Forum forum)
    {
        return forum.ToSummary();
    }
}