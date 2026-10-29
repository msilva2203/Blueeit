using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public interface IForumThreadRepository
{
    ForumThread? GetById(int id);

    Task<ForumThread?> GetByIdAsync(int id);

    ForumThread? GetLatestByForumId(int forumId);

    IEnumerable<ForumThread> GetAll();

    Task<IEnumerable<ForumThread>> GetAllAsync();

    int GetCount();

    Task<int> GetCountAsync();

    int GetCountByForumId(int forumId);

    ForumThread Create(int forumId, int authorId, string title, string content);

    Task<ForumThread> CreateAsync(int forumId, int authorId, string title, string content);

    bool DeleteById(int id);

    Task<bool> DeleteByIdAsync(int id);
}