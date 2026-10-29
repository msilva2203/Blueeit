using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ProfilePostMappings
{
    public static ProfilePostResponse ToResponse(
        this ProfilePost profilePost,
        UserSummary user,
        UserSummary author)
    {
        return new ProfilePostResponse
        {
            Id = profilePost.Id,
            User = user,
            Author = author,
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