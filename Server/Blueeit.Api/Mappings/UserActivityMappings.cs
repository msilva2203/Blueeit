using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.DTOs.UserActivity;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ActivityMappings
{
    public static UserActivityResponse ToResponse(
        this UserActivity activity,
        UserSummary user,
        ForumSummary? forum,
        ForumThreadSummary? thread,
        PostResponse? post,
        ProfilePostResponse? profilePost,
        UserSummary? profileOwner)
    {
        return new UserActivityResponse
        {
            Id = activity.Id,
            User = user,
            Type = activity.Type,
            CreatedAt = activity.CreatedAt,
            Forum = forum,
            Thread = thread,
            Post = post,
            ProfilePost = profilePost,
            ProfileOwner = profileOwner
        };
    }
}