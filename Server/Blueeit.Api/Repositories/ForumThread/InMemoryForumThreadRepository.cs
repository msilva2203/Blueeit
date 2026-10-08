using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public class InMemoryForumThreadRepository : IForumThreadRepository
{
    private readonly List<ForumThread> _threads = [];
    private int _nextId = 1;

    public ForumThread? GetById(int id)
    {
        return _threads.FirstOrDefault(thread => thread.Id == id);
    }

    public Task<ForumThread?> GetByIdAsync(int id)
    {
        return Task.FromResult(GetById(id));
    }

    public IEnumerable<ForumThread> GetAll()
    {
        return _threads;
    }

    public Task<IEnumerable<ForumThread>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<ForumThread>>(_threads);
    }

    public int GetCount()
    {
        return _threads.Count();
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(GetCount());
    }

    public ForumThread Create(int forumId, int authorId, string title, string content)
    {
        var thread = new ForumThread
        {
            Id = _nextId++,
            AuthorId = authorId,
            ForumId = forumId,
            CreatedAt = DateTime.UtcNow,
            Title = title
        };

        _threads.Add(thread);

        return thread;
    }

    public Task<ForumThread> CreateAsync(int forumId, int authorId, string title, string content)
    {
        return Task.FromResult(Create(forumId, authorId, title, content));
    }

    public bool DeleteById(int id)
    {
        ForumThread? thread = _threads.FirstOrDefault(thread => thread.Id == id);

        if (thread is null)
        {
            return false;
        }

        var result = _threads.Remove(thread);

        return result;
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        return Task.FromResult(DeleteById(id));
    }
}