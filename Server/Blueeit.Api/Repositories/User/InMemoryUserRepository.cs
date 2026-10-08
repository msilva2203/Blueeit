using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = [];
    private int _nextId = 1;

    public User? GetById(int id)
    {
        return _users.FirstOrDefault(user => user.Id == id);
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return Task.FromResult(GetById(id));
    }

    public User? GetLatest()
    {
        return _users
            .OrderByDescending(user => user.CreatedAt)
            .FirstOrDefault();
    }

    public Task<User?> GetLatestAsync()
    {
        return Task.FromResult(GetLatest());
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }

    public int GetCount()
    {
        return _users.Count();
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(GetCount());
    }

    public User Create(string username, string email, string password)
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

        return user;
    }

    public Task<User> CreateAsync(string username, string email, string password)
    {
        return Task.FromResult(Create(username, email, password));
    }

    public bool DeleteById(int id)
    {
        User? user = _users.FirstOrDefault(user => user.Id == id);

        if (user is null)
        {
            return false;
        }

        var result = _users.Remove(user);

        return result;
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        return Task.FromResult(DeleteById(id));
    }
}