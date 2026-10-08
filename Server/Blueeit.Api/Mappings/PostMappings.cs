using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class PostMappings
{
    public static PostResponse ToResponse(
        this Post post,
        ForumThreadSummary threadSummary,
        UserSummary authorSummary)
    {
        return new PostResponse
        {
            Id = post.Id,
            Thread = threadSummary,
            Author = authorSummary,
            CreatedAt = post.CreatedAt,
            Content = post.Content
        };
    }

    public static PostSummary ToSummary(
        this Post post)
    {
        return new PostSummary
        {
            Id = post.Id,
            CreatedAt = post.CreatedAt
        };
    }
}
