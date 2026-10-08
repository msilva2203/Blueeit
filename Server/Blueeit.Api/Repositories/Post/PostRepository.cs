using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public interface IPostRepository
{
    Post? GetById(int id);

    Task<Post?> GetByIdAsync(int id);

    IEnumerable<Post> GetAll();

    Task<IEnumerable<Post>> GetAllAsync();

    int GetCount();

    Task<int> GetCountAsync();

    Post Create(int threadId, int authorId, string content);

    Task<Post> CreateAsync(int threadId, int authorId, string content);

    bool DeleteById(int id);

    Task<bool> DeleteByIdAsync(int id);
}