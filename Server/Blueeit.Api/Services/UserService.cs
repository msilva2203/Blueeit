using Blueeit.Api.Common.Pagination;
using Blueeit.Api.Models;

namespace Blueeit.Api.Services;

/*
 *
 */
public interface IUserService
{
    Task<User?> GetUserByIdAsync(int id);

    Task<int> GetCountAsync();

    Task<PaginatedResult<User>> GetAllUsersAsync(int page, int pageSize);

    Task<User> CreateUserAsync(string username, string email, string password);

    Task<bool> DeleteUserAsync(int id);
}

/*
 *
 */
public class InMemoryUserService : IUserService
{
    private readonly List<User> _users = [];
    private int _nextId = 1;

    public Task<User?> GetUserByIdAsync(int id)
    {
        User? user = _users.FirstOrDefault(user => user.Id == id);
        return Task.FromResult(user);
    }

    public Task<int> GetCountAsync()
    {
        var count = _users.Count();
        return Task.FromResult(count);
    }

    public Task<PaginatedResult<User>> GetAllUsersAsync(int page, int pageSize)
    {
        int totalCount = _users.Count();

        IReadOnlyList<User> users = _users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<User>
        {
            Items = users,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<User> CreateUserAsync(string username, string email, string password)
    {
        var user = new User
        {
            Id = _nextId,
            Username = username,
            Email = email,
            Password = password,
            Date = DateTime.Now
        };

        _users.Add(user);
        _nextId++;

        return Task.FromResult(user);
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
