using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.ProfilePost;
using Blueeit.Api.Repositories;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public class InMemoryProfilePostService : IProfilePostService
{
    private readonly IProfilePostRepository _profilePostRepository;
    private readonly IUserActivityRepository _activityRepository;

    public InMemoryProfilePostService(
        IProfilePostRepository profilePostRepository,
        IUserActivityRepository activityRepository)
    {
        _profilePostRepository = profilePostRepository;
        _activityRepository = activityRepository;
    }

    public async Task<ProfilePostResponse?> GetProfilePostByIdAsync(int id)
    {
        ProfilePost? profilePost = await _profilePostRepository.GetByIdAsync(id);

        if (profilePost is null)
        {
            return null;
        }

        var result = profilePost.ToResponse();

        return result;
    }

    public async Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsAsync(ProfilePostQueryKey queryKey, int page, int pageSize)
    {
        var all = await _profilePostRepository.GetAllAsync();

        var profilePosts = all
            .Select(profilePost => profilePost.ToResponse());

        var totalCount = profilePosts.Count();

        profilePosts = queryKey switch
        {
            ProfilePostQueryKey.None =>
                profilePosts,

            ProfilePostQueryKey.Newest =>
                profilePosts.OrderByDescending(profilePost => profilePost.CreatedAt),

            _ => profilePosts
        };

        var items = profilePosts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ProfilePostResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsByUserIdAsync(ProfilePostQueryKey queryKey, int userId, int page, int pageSize)
    {
        var all = await _profilePostRepository.GetAllAsync();

        var profilePosts = all
            .Where(profilePost => profilePost.UserId == userId)
            .Select(profilePost => profilePost.ToResponse());

        var totalCount = profilePosts.Count();

        profilePosts = queryKey switch
        {
            ProfilePostQueryKey.None =>
                profilePosts,

            ProfilePostQueryKey.Newest =>
                profilePosts.OrderByDescending(profilePost => profilePost.CreatedAt),

            _ => profilePosts
        };

        var items = profilePosts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ProfilePostResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsByAuthorIdAsync(ProfilePostQueryKey queryKey, int authorId, int page, int pageSize)
    {
        var all = await _profilePostRepository.GetAllAsync();

        var profilePosts = all
            .Where(profilePost => profilePost.AuthorId == authorId)
            .Select(profilePost => profilePost.ToResponse());

        var totalCount = profilePosts.Count();

        profilePosts = queryKey switch
        {
            ProfilePostQueryKey.None =>
                profilePosts,

            ProfilePostQueryKey.Newest =>
                profilePosts.OrderByDescending(profilePost => profilePost.CreatedAt),

            _ => profilePosts
        };

        var items = profilePosts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ProfilePostResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<ProfilePostResponse> CreateProfilePostAsync(int userId, int authorId, string content)
    {
        var profilePost = await _profilePostRepository.CreateAsync(
            userId,
            authorId,
            content
        );
        
        var activity = await _activityRepository.CreateAsync(
            CreateUserActivityData.ProfilePostCreated(
                profilePost.AuthorId,
                profilePost.CreatedAt,
                profilePost.Id,
                profilePost.UserId
            )
        );

        var result = profilePost.ToResponse();

        return result;
    }

    public async Task<bool> DeleteProfilePostAsync(int id)
    {
        return await _profilePostRepository.DeleteByIdAsync(id);
    }
}