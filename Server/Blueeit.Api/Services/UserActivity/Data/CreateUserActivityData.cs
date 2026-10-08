using Blueeit.Api.Models;

namespace Blueeit.Api.Services.Data;

public readonly record struct CreateUserActivityData
{
    public int UserId { get; init; }
    public UserActivityType Type { get; init; }
    public DateTime CreatedAt { get; init; }
    public int? ForumId { get; init; }
    public int? ThreadId { get; init; }
    public int? PostId { get; init; }
    public int? ProfilePostId { get; init; }
    public int? ProfileOwnerId { get; init; }

    public static CreateUserActivityData ForumCreated(
        int userId,
        DateTime createdAt,
        int forumId)
    {
        return new CreateUserActivityData
        {
            UserId = userId,
            Type = UserActivityType.ForumCreated,
            CreatedAt = createdAt,
            ForumId = forumId
        };
    }

    public static CreateUserActivityData ThreadCreated(
        int userId,
        DateTime createdAt,
        int forumId,
        int threadId)
    {
        return new CreateUserActivityData
        {
            UserId = userId,
            Type = UserActivityType.ThreadCreated,
            CreatedAt = createdAt,
            ForumId = forumId,
            ThreadId = threadId
        };
    }

    public static CreateUserActivityData PostCreated(
        int userId,
        DateTime createdAt,
        int threadId,
        int postId)
    {
        return new CreateUserActivityData
        {
            UserId = userId,
            Type = UserActivityType.PostCreated,
            CreatedAt = createdAt,
            ThreadId = threadId,
            PostId = postId
        };
    }

    public static CreateUserActivityData ProfilePostCreated(
        int userId,
        DateTime createdAt,
        int profilePostId,
        int ProfileOwnerId)
    {
        return new CreateUserActivityData
        {
            UserId = userId,
            Type = UserActivityType.ProfilePostCreated,
            CreatedAt = createdAt,
            ProfilePostId = profilePostId,
            ProfileOwnerId = ProfileOwnerId
        };
    }
};