using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.ProfilePost;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public class InMemoryProfilePostService : IProfilePostService
{
    private readonly IUserActivityService _activityService;
    private readonly List<ProfilePost> _profilePosts = [];
    private int _nextId = 1;

    public InMemoryProfilePostService(IUserActivityService activityService)
    {
        _activityService = activityService;
    }

    public Task<ProfilePostResponse?> GetProfilePostByIdAsync(int id)
    {
        ProfilePost? profilePost = _profilePosts.FirstOrDefault(profilePost => profilePost.Id == id);

        if (profilePost is null)
        {
            return Task.FromResult<ProfilePostResponse?>(null);
        }

        var result = profilePost.ToResponse();

        return Task.FromResult<ProfilePostResponse?>(result);
    }

    public int GetCountByAuthorId(int authorId)
    {
        return _profilePosts.Count(profilePost => profilePost.AuthorId == authorId);
    }

    public Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsAsync(ProfilePostQueryKey queryKey, int page, int pageSize)
    {
        var profilePosts = _profilePosts
            .Select(profilePost => profilePost.ToResponse());

        var totalCount = profilePosts.Count();

        profilePosts = queryKey switch
        {
            ProfilePostQueryKey.None =>
                profilePosts,

            ProfilePostQueryKey.Newest =>
                profilePosts.OrderByDescending(profilePost => profilePost.CreationDate),

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

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsByUserIdAsync(ProfilePostQueryKey queryKey, int userId, int page, int pageSize)
    {
        var profilePosts = _profilePosts
            .Where(profilePost => profilePost.UserId == userId)
            .Select(profilePost => profilePost.ToResponse());

        var totalCount = profilePosts.Count();

        profilePosts = queryKey switch
        {
            ProfilePostQueryKey.None =>
                profilePosts,

            ProfilePostQueryKey.Newest =>
                profilePosts.OrderByDescending(profilePost => profilePost.CreationDate),

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

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsByAuthorIdAsync(ProfilePostQueryKey queryKey, int authorId, int page, int pageSize)
    {
        var profilePosts = _profilePosts
            .Where(profilePost => profilePost.AuthorId == authorId)
            .Select(profilePost => profilePost.ToResponse());

        var totalCount = profilePosts.Count();

        profilePosts = queryKey switch
        {
            ProfilePostQueryKey.None =>
                profilePosts,

            ProfilePostQueryKey.Newest =>
                profilePosts.OrderByDescending(profilePost => profilePost.CreationDate),

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

        return Task.FromResult(result);
    }

    public async Task<ProfilePostResponse> CreateProfilePostAsync(int userId, int authorId, string content)
    {
        var profilePost = new ProfilePost
        {
            Id = _nextId++,
            UserId = userId,
            AuthorId = authorId,
            CreationDate = DateTime.UtcNow,
            Content = content
        };

        _profilePosts.Add(profilePost);
        
        await _activityService.CreateAsync(
            CreateUserActivityData.ProfilePostCreated(
                profilePost.AuthorId,
                profilePost.CreationDate,
                profilePost.Id,
                profilePost.UserId
            )
        );

        var result = profilePost.ToResponse();

        return result;
    }

    public Task<bool> DeleteProfilePostAsync(int id)
    {
        ProfilePost? profilePost = _profilePosts.FirstOrDefault(profilePost => profilePost.Id == id);

        if (profilePost is null)
        {
            return Task.FromResult(false);
        }

        var result = _profilePosts.Remove(profilePost);

        return Task.FromResult(result);
    }
}