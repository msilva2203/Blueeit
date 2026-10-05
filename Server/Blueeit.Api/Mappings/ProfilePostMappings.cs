using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ProfilePostMappings
{
    public static ProfilePostResponse ToResponse(this ProfilePost profilePost)
    {
        return new ProfilePostResponse
        {
            Id = profilePost.Id,
            UserId = profilePost.UserId,
            AuthorId = profilePost.AuthorId,
            CreationDate = profilePost.CreationDate,
            Content = profilePost.Content
        };
    }
}