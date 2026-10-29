using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ForumMappings
{
    public static ForumResponse ToResponse(
        this Forum forum,
        UserSummary author,
        ForumSummary? parent,
        ForumThreadSummary? latestThread,
        ForumMetadata metadata)
    {
        return new ForumResponse
        {
            Id = forum.Id,
            Author = author,
            Parent = parent,
            LatestThread = latestThread,
            CreatedAt = forum.CreatedAt,
            Title = forum.Title,
            Metadata = metadata
        };
    }

    public static ForumSummary ToSummary(
        this Forum forum)
    {
        return new ForumSummary
        {
            Id = forum.Id,
            CreatedAt = forum.CreatedAt,
            Title = forum.Title
        };
    }
}