using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ForumThreadMappings
{
    public static ForumThreadResponse ToResponse(
        this ForumThread thread)
    {
        return new ForumThreadResponse
        {
            Id = thread.Id,
            AuthorId = thread.AuthorId,
            ForumId = thread.ForumId,
            OpeningPostId = thread.OpeningPostId,
            CreatedAt = thread.CreatedAt,
            Title = thread.Title
        };
    }

    public static ForumThreadSummary ToSummary(
        this ForumThread thread)
    {
        return new ForumThreadSummary
        {
            Id = thread.Id,
            AuthorId = thread.AuthorId,
            CreatedAt = thread.CreatedAt,
            Title = thread.Title
        };
    }
}
