using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ForumThreadMappings
{
    public static ForumThreadResponse ToResponse(this ForumThread thread)
    {
        return new ForumThreadResponse
        {
            Id = thread.Id,
            AuthorId = thread.AuthorId,
            ForumId = thread.ForumId,
            OpeningPostId = thread.OpeningPostId,
            CreationDate = thread.CreationDate,
            Title = thread.Title
        };
    }
}
