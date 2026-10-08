using Blueeit.Api.DTOs.UserActivity;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ActivityMappings
{
    public static UserActivityResponse ToResponse(this UserActivity activity)
    {
        return new UserActivityResponse
        {
            Id = activity.Id,
            UserId = activity.UserId,
            Type = activity.Type,
            CreatedAt = activity.CreatedAt,
            ForumId = activity.ForumId,
            ThreadId = activity.ThreadId,
            PostId = activity.PostId,
            ProfilePostId = activity.ProfilePostId,
            ProfileOwnerId = activity.ProfileOwnerId
        };
    }
}