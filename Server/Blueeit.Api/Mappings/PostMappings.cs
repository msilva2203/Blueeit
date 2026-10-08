using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class PostMappings
{
    public static PostResponse ToResponse(
        this Post post)
    {
        return new PostResponse
        {
            Id = post.Id,
            ThreadId = post.ThreadId,
            AuthorId = post.AuthorId,
            CreatedAt = post.CreatedAt,
            Content = post.Content
        };
    }

    public static PostSummary ToSummary(this Post post)
    {
        return new PostSummary
        {
            Id = post.Id,
            CreatedAt = post.CreatedAt
        };
    }
}
