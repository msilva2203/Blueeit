using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public interface IProfilePostFactoryService
{
    ProfilePostResponse MakeResponse(ProfilePost profilePost);

    ProfilePostSummary MakeSummary(ProfilePost profilePost);
}

public class ProfilePostFactoryService : IProfilePostFactoryService
{
    private readonly IUserRepository _userRepository;

    public ProfilePostFactoryService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ProfilePostResponse MakeResponse(ProfilePost profilePost)
    {
        var user = _userRepository.GetById(profilePost.UserId);
        var author = _userRepository.GetById(profilePost.AuthorId);

        if (user is null)
        {
            throw new InvalidOperationException(
                $"Profile post {profilePost.Id} references a missing user {profilePost.UserId}.");
        }

        if (author is null)
        {
            throw new InvalidOperationException(
                $"Profile post {profilePost.Id} references a missing author {profilePost.AuthorId}.");
        }

        return profilePost.ToResponse(
            user.ToSummary(),
            author.ToSummary()
        );
    }

    public ProfilePostSummary MakeSummary(ProfilePost profilePost)
    {
        return profilePost.ToSummary();
    }
}