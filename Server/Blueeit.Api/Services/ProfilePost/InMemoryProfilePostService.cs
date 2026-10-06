using Blueeit.Api.Common.Pagination;
using Blueeit.Api.Models;

namespace Blueeit.Api.Services;

public class InMemoryProfilePostService : IProfilePostService
{
    private readonly List<ProfilePost> _profilePosts = [];
    private int _nextId = 1;

    public Task<ProfilePost?> GetProfilePostByIdAsync(int id)
    {
        ProfilePost? profilePost = _profilePosts.FirstOrDefault(profilePost => profilePost.Id == id);
        return Task.FromResult(profilePost);
    }

    public int GetCountByAuthorId(int authorId)
    {
        return _profilePosts.Count(profilePost => profilePost.AuthorId == authorId);
    }

    public Task<PaginatedResult<ProfilePost>> GetProfilePostsAsync(int page, int pageSize)
    {
        var totalCount = _profilePosts.Count();

        var profilePosts = _profilePosts
            .Skip((page - 1) * pageSize)
            .Skip(pageSize)
            .ToList();

        var result = new PaginatedResult<ProfilePost>
        {
            Items = profilePosts,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<ProfilePost>> GetProfilePostsByUserIdAsync(int userId, int page, int pageSize)
    {
        var profilePosts = _profilePosts
            .Where(profilePost => profilePost.UserId == userId);

        var totalCount = profilePosts.Count();

        var items = profilePosts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ProfilePost>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<ProfilePost>> GetProfilePostsByAuthorIdAsync(int authorId, int page, int pageSize)
    {
        var profilePosts = _profilePosts
            .Where(profilePost => profilePost.AuthorId == authorId);

        var totalCount = profilePosts.Count();

        var items = profilePosts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ProfilePost>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<ProfilePost> CreateProfilePostAsync(int userId, int authorId, string content)
    {
        var newProfilePost = new ProfilePost
        {
            Id = _nextId,
            UserId = userId,
            AuthorId = authorId,
            CreationDate = DateTime.UtcNow,
            Content = content
        };

        _profilePosts.Add(newProfilePost);
        _nextId++;

        return Task.FromResult(newProfilePost);
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