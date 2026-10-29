using Blueeit.Api.DTOs.User;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public interface IUserFactoryService
{
    UserMetadata MakeMetadata(User user);

    UserResponse MakeResponse(User user);

    UserSummary MakeSummary(User user);
}

public class UserFactoryService : IUserFactoryService
{
    private readonly IPostRepository _postRepository;
    private readonly IProfilePostRepository _profilePostRepository;

    public UserFactoryService(
        IPostRepository postRepository,
        IProfilePostRepository profilePostRepository)
    {
        _postRepository = postRepository;
        _profilePostRepository = profilePostRepository;
    }

    public UserMetadata MakeMetadata(User user)
    {
        return new UserMetadata
        {
            PostCount = _postRepository.GetCountByAuthorId(user.Id),
            ProfilePostCount = _profilePostRepository.GetCountByAuthorId(user.Id)
        };
    }

    public UserResponse MakeResponse(User user)
    {
        return user.ToResponse(
            MakeMetadata(user)
        );
    }

    public UserSummary MakeSummary(User user)
    {
        return user.ToSummary();
    }
}