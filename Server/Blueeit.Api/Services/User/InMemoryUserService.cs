using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.User;

namespace Blueeit.Api.Services;

public class InMemoryUserService : IUserService
{
    private readonly IPostService _postService;
    private readonly IProfilePostService _profilePostService;
    private readonly List<User> _users = [];
    private int _nextId = 1;

    public InMemoryUserService(
        IPostService postService, 
        IProfilePostService profilePostService)
    {
        _postService = postService;
        _profilePostService = profilePostService;
    }

    private UserMetadata GetUserMetadata(User user)
    {
        return new UserMetadata
        {
            PostCount = _postService.GetCountByAuthorId(user.Id),
            ProfilePostCount = _profilePostService.GetCountByAuthorId(user.Id)
        };
    }

    public Task<UserResponse?> GetUserByIdAsync(int id)
    {
        User? user = _users.FirstOrDefault(user => user.Id == id);

        if (user is null)
        {
            return Task.FromResult<UserResponse?>(null);
        }

        var result = user.ToResponse(GetUserMetadata(user));

        return Task.FromResult<UserResponse?>(result);
    }

    public Task<UserResponse?> GetLatestUser()
    {
        User? user = _users
            .OrderByDescending(user => user.CreatedAt)
            .FirstOrDefault();

        if (user is null)
        {
            return Task.FromResult<UserResponse?>(null);
        }

        var result = user.ToResponse(GetUserMetadata(user));

        return Task.FromResult<UserResponse?>(result);
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(_users.Count());
    }

    public Task<PaginatedResult<UserResponse>> GetAllUsersAsync(UserQueryKey sortKey, int page, int pageSize)
    {
        var users = _users
            .Select(user => user.ToResponse(GetUserMetadata(user)));

        int totalCount = users.Count();

        users = sortKey switch
        {
            UserQueryKey.None => 
                users,

            UserQueryKey.Newest =>
                users.OrderByDescending(user => user.CreatedAt),

            UserQueryKey.Oldest =>
                users.OrderBy(user => user.CreatedAt),

            UserQueryKey.MostMessages =>
                users.OrderByDescending(user =>
                    user.Metadata.PostCount + user.Metadata.ProfilePostCount),

            _ => users
        };

        var items = users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<UserResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<UserResponse> CreateUserAsync(string username, string email, string password)
    {
        var user = new User
        {
            Id = _nextId++,
            Username = username,
            Email = email,
            Password = password,
            CreatedAt = DateTime.UtcNow
        };

        _users.Add(user);

        var result = user.ToResponse(new UserMetadata());

        return Task.FromResult(result);
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        User? user = _users.FirstOrDefault(user => user.Id == id);

        if (user is null)
        {
            return Task.FromResult(false);
        }

        var result = _users.Remove(user);

        return Task.FromResult(result);
    }
}