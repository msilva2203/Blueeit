using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ForumThreadMappings
{
    public static ForumThreadResponse ToResponse(
        this ForumThread thread,
        UserSummary author,
        ForumSummary forum,
        PostSummary openingPost)
    {
        return new ForumThreadResponse
        {
            Id = thread.Id,
            Author = author,
            Forum = forum,
            OpeningPost = openingPost,
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
