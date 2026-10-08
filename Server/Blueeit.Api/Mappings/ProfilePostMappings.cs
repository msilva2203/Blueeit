using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ProfilePostMappings
{
    public static ProfilePostResponse ToResponse(
        this ProfilePost profilePost)
    {
        return new ProfilePostResponse
        {
            Id = profilePost.Id,
            UserId = profilePost.UserId,
            AuthorId = profilePost.AuthorId,
            CreatedAt = profilePost.CreatedAt,
            Content = profilePost.Content
        };
    }

    public static ProfilePostSummary ToSummary(
        this ProfilePost profilePost)
    {
        return new ProfilePostSummary
        {
            Id = profilePost.Id,
            CreatedAt = profilePost.CreatedAt
        };
    }
}