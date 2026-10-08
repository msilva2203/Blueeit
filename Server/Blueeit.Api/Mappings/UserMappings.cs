using Blueeit.Api.DTOs.User;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class UserMappings
{
    public static UserResponse ToResponse(
        this User user, 
        UserMetadata metadata)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            Metadata = metadata
        };
    }

    public static UserSummary ToSummary(
        this User user)
    {
        return new UserSummary
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }
}
