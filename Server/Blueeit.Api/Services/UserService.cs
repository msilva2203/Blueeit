using System.Security.Cryptography.X509Certificates;
using Blueeit.Api.Models;

namespace Blueeit.Api.Services;

/*
 *
 */
public interface IUserService
{
    User? GetUserByIdAsync(int id);

    IReadOnlyList<User> GetAllUsers();

    User CreateUserAsync(string username, string email);

    bool DeleteUserAsync(int id);
}

/*
 *
 */
public class InMemoryUserService : IUserService
{
    private readonly List<User> _users = [];
    private int _nextId = 1;

    public User? GetUserByIdAsync(int id)
    {
        return _users.FirstOrDefault(user => user.Id == id);
    }

    public IReadOnlyList<User> GetAllUsers()
    {
        return _users;
    }

    public User CreateUserAsync(string username, string email)
    {
        var user = new User
        {
            Id = _nextId,
            Username = username,
            Email = email,
            Date = DateTime.Now
        };

        _nextId++;

        _users.Add(user);

        return user;
    }

    public bool DeleteUserAsync(int id)
    {
        var user = GetUserByIdAsync(id);

        if (user is null)
        {
            return false;
        }

        var result = _users.Remove(user);

        return result;
    }
}
