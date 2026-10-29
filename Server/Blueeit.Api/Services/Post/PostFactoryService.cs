using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public interface IPostFactoryService
{
    PostResponse MakeResponse(Post post);

    PostSummary MakeSummary(Post post);
}

public class PostFactoryService : IPostFactoryService
{
    private readonly IForumThreadRepository _threadRepository;
    private readonly IUserRepository _userRepository;

    public PostFactoryService(
        IForumThreadRepository threadRepository,
        IUserRepository userRepository)
    {
        _threadRepository = threadRepository;
        _userRepository = userRepository;
    }

    public PostResponse MakeResponse(Post post)
    {
        var thread = _threadRepository.GetById(post.ThreadId);
        var author = _userRepository.GetById(post.AuthorId);

        if (thread is null)
        {
            throw new InvalidOperationException(
                $"Post {post.Id} references a missing thread {post.ThreadId}.");
        }

        if (author is null)
        {
            throw new InvalidOperationException(
                $"Post {post.Id} references a missing author {post.AuthorId}.");
        }

        return post.ToResponse(
            thread.ToSummary(),
            author.ToSummary()
        );
    }

    public PostSummary MakeSummary(Post post)
    {
        return post.ToSummary();
    }
}