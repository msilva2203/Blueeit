using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ForumMappings
{
    public static ForumResponse ToResponse(
        this Forum forum,
        ForumMetadata metadata)
    {
        return new ForumResponse
        {
            Id = forum.Id,
            ParentId = forum.ParentId,
            AuthorId = forum.AuthorId,
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