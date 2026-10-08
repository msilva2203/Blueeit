using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public class InMemoryForumRepository : IForumRepository
{
    private readonly List<Forum> _forums = [];
    private int _nextId = 1;

    public Forum? GetById(int id)
    {
        return _forums.FirstOrDefault(forum => forum.Id == id);
    }

    public Task<Forum?> GetByIdAsync(int id)
    {
        return Task.FromResult(GetById(id));
    }

    public IEnumerable<Forum> GetAll()
    {
        return _forums;
    }

    public Task<IEnumerable<Forum>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Forum>>(_forums);
    }

    public int GetCount()
    {
        return _forums.Count();
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(GetCount());
    }

    public Forum Create(int? parentId, int authorId, string title)
    {
        var forum = new Forum
        {
            Id = _nextId++,
            ParentId = parentId,
            AuthorId = authorId,
            CreatedAt = DateTime.UtcNow,
            Title = title
        };

        _forums.Add(forum);

        return forum;
    }

    public Task<Forum> CreateAsync(int? parentId, int authorId, string title)
    {
        return Task.FromResult(Create(parentId, authorId, title));
    }

    public bool DeleteById(int id)
    {
        Forum? forum = _forums.FirstOrDefault(forum => forum.Id == id);

        if (forum is null)
        {
            return false;
        }

        var result = _forums.Remove(forum);

        return result;
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        return Task.FromResult(DeleteById(id));
    }
}