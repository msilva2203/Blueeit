using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public interface IForumThreadFactoryService
{
    ForumThreadResponse MakeResponse(ForumThread thread);

    ForumThreadSummary MakeSummary(ForumThread thread);
}

public class ForumThreadFactoryService : IForumThreadFactoryService
{
    private readonly IUserRepository _userRepository;
    private readonly IForumRepository _forumRepository;
    private readonly IPostRepository _postRepository;

    public ForumThreadFactoryService(
        IUserRepository userRepository,
        IForumRepository forumRepository,
        IPostRepository postRepository)
    {
        _userRepository = userRepository;
        _forumRepository = forumRepository;
        _postRepository = postRepository;
    }

    public ForumThreadResponse MakeResponse(ForumThread thread)
    {
        var author = _userRepository.GetById(thread.AuthorId);
        var forum = _forumRepository.GetById(thread.ForumId);
        var openingPost = _postRepository.GetById(thread.OpeningPostId);

        if (author is null)
        {
            throw new InvalidOperationException(
                $"Forum thread {thread.Id} references a missing author {thread.AuthorId}.");
        }

        if (forum is null)
        {
            throw new InvalidOperationException(
                $"Forum thread {thread.Id} references a missing forum {thread.ForumId}.");
        }

        if (openingPost is null)
        {
            throw new InvalidOperationException(
                $"Forum thread {thread.Id} references a missing opening post {thread.OpeningPostId}.");
        }

        return thread.ToResponse(
            author.ToSummary(),
            forum.ToSummary(),
            openingPost.ToSummary()
        );
    }

    public ForumThreadSummary MakeSummary(ForumThread thread)
    {
        return thread.ToSummary();
    }
}