using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public interface IForumRepository
{
    Forum? GetById(int id);

    Task<Forum?> GetByIdAsync(int id);

    IEnumerable<Forum> GetAll();

    Task<IEnumerable<Forum>> GetAllAsync();

    int GetCount();

    Task<int> GetCountAsync();

    Forum Create(int? parentId, int authorId, string title);

    Task<Forum> CreateAsync(int? parentId, int authorId, string title);

    bool DeleteById(int id);

    Task<bool> DeleteByIdAsync(int id);
}