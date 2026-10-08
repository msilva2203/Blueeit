using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public interface IProfilePostRepository
{
    ProfilePost? GetById(int id);

    Task<ProfilePost?> GetByIdAsync(int id);

    IEnumerable<ProfilePost> GetAll();

    Task<IEnumerable<ProfilePost>> GetAllAsync();

    int GetCount();

    Task<int> GetCountAsync();

    ProfilePost Create(int userId, int authorId, string content);

    Task<ProfilePost> CreateAsync(int userId, int authorId, string content);

    bool DeleteById(int id);

    Task<bool> DeleteByIdAsync(int id);
}