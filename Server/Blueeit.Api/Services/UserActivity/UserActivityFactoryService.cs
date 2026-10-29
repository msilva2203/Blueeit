using Blueeit.Api.DTOs.UserActivity;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public interface IUserActivityFactoryService
{
    UserActivityResponse MakeResponse(UserActivity activity);
}

public class UserActivityFactoryService : IUserActivityFactoryService
{
    private readonly IUserRepository _userRepository;
    private readonly IForumRepository _forumRepository;
    private readonly IForumThreadRepository _threadRepository;
    private readonly IPostRepository _postRepository;
    private readonly IProfilePostRepository _profilePostRepository;

    public UserActivityFactoryService(
        IUserRepository userRepository,
        IForumRepository forumRepository,
        IForumThreadRepository threadRepository,
        IPostRepository postRepository,
        IProfilePostRepository profilePostRepository)
    {
        _userRepository = userRepository;
        _forumRepository = forumRepository;
        _threadRepository = threadRepository;
        _postRepository = postRepository;
        _profilePostRepository = profilePostRepository;
    }

    private UserActivityResponse MakeForumCreatedResponse(UserActivity activity, User user)
    {
        Forum? forum = _forumRepository.GetById(activity.ForumId.Value);

        return activity.ToResponse(
            user.ToSummary(),
            forum.ToSummary(),
            null,
            null,
            null,
            null
        );
    }

    private UserActivityResponse MakeThreadCreatedResponse(UserActivity activity, User user)
    {
        Forum? forum = _forumRepository.GetById(activity.ForumId.Value);

        ForumThread? thread = _threadRepository.GetById(activity.ThreadId.Value);

        return activity.ToResponse(
            user.ToSummary(),
            forum.ToSummary(),
            thread.ToSummary(),
            null,
            null,
            null
        );
    }

    private UserActivityResponse MakePostCreatedResponse(UserActivity activity, User user)
    {
        ForumThread? thread = _threadRepository.GetById(activity.ThreadId.Value);

        Post? post = _postRepository.GetById(activity.PostId.Value);

        var userSummary = user.ToSummary();

        var threadSummary = thread.ToSummary();

        var postResponse = post.ToResponse(
            threadSummary,
            userSummary
        );

        return activity.ToResponse(
            userSummary,
            null,
            threadSummary,
            postResponse,
            null,
            null
        );
    }

    private UserActivityResponse MakeProfilePostCreatedResponse(UserActivity activity, User user)
    {
        ProfilePost? profilePost = _profilePostRepository.GetById(activity.ProfilePostId.Value);

        User? profileOwner = _userRepository.GetById(activity.ProfileOwnerId.Value);

        var userSummary = user.ToSummary();

        var profileOwnerSummary = profileOwner.ToSummary();

        var profilePostResponse = profilePost.ToResponse(
            profileOwnerSummary,
            userSummary
        );

        return activity.ToResponse(
            userSummary,
            null,
            null,
            null,
            profilePostResponse,
            profileOwnerSummary
        );
    }

    public UserActivityResponse MakeResponse(UserActivity activity)
    {
        User? user = _userRepository.GetById(activity.UserId);

        switch (activity.Type)
        {
            case UserActivityType.ForumCreated:
                return MakeForumCreatedResponse(
                    activity,
                    user
                );

            case UserActivityType.ThreadCreated:
                return MakeThreadCreatedResponse(
                    activity,
                    user
                );

            case UserActivityType.PostCreated:
                return MakePostCreatedResponse(
                    activity,
                    user
                );

            case UserActivityType.ProfilePostCreated:
                return MakeProfilePostCreatedResponse(
                    activity,
                    user
                );

            default:
                throw new InvalidOperationException();
        }
    }
}