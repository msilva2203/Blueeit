using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class PostMappings
{
    public static PostResponse ToResponse(this Post post)
    {
        return new PostResponse
        {
            Id = post.Id,
            ThreadId = post.ThreadId,
            AuthorId = post.AuthorId,
            CreationDate = post.CreationDate,
            Content = post.Content
        };
    }
}
