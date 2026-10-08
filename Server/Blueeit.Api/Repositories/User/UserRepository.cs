using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public interface IUserRepository
{
    User? GetById(int id);

    Task<User?> GetByIdAsync(int id);

    User? GetLatest();

    Task<User?> GetLatestAsync();

    IEnumerable<User> GetAll();

    Task<IEnumerable<User>> GetAllAsync();

    int GetCount();

    Task<int> GetCountAsync();

    User Create(string username, string email, string password);

    Task<User> CreateAsync(string username, string email, string password);

    bool DeleteById(int id);

    Task<bool> DeleteByIdAsync(int id);
}