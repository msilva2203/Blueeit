using Blueeit.Api.Models;
using Blueeit.Api.Common.Pagination;

namespace Blueeit.Api.Services;

public interface IForumService
{
    Task<Forum?> GetForumByIdAsync(int id);

    Task<int> GetCountAsync();

    Task<PaginatedResult<Forum>> GetAllForumsAsync(int page, int pageSize);

    Task<PaginatedResult<Forum>> GetRootForumsAsync(int page, int pageSize);

    Task<PaginatedResult<Forum>> GetSubforumsAsync(int id, int page, int pageSize);

    Task<Forum> CreateForumAsync(int? parentId, int authorId, string title);

    Task<bool> DeleteForumAsync(int id);
}

public class InMemoryForumService : IForumService
{
    private readonly List<Forum> _forums = [];
    private int _nextId = 1;

    public Task<Forum?> GetForumByIdAsync(int id)
    {
        Forum? forum = _forums.FirstOrDefault(forum => forum.Id == id);
        return Task.FromResult(forum);
    }

    public Task<int> GetCountAsync()
    {
        var count = _forums.Count();
        return Task.FromResult(count);
    }

    public Task<PaginatedResult<Forum>> GetAllForumsAsync(int page, int pageSize)
    {
        int totalCount = _forums.Count();

        IReadOnlyList<Forum> forums = _forums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<Forum>
        {
            Items = forums,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<Forum>> GetRootForumsAsync(int page, int pageSize)
    {
        var forums = _forums
            .Where(forum => forum.ParentId == null);

        var totalCount = forums.Count();

        var items = forums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<Forum>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<Forum>> GetSubforumsAsync(int id, int page, int pageSize)
    {
        var subforums = _forums
            .Where(forum => forum.ParentId == id);

        int totalCount = subforums.Count();

        var items = subforums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<Forum>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<Forum> CreateForumAsync(int? parentId, int authorId, string title)
    {
        var forum = new Forum
        {
            Id = _nextId,
            ParentId = parentId,
            AuthorId = authorId,
            CreationDate = DateTime.Now,
            Title = title
        };

        _forums.Add(forum);
        _nextId++;

        return Task.FromResult(forum);
    }

    public Task<bool> DeleteForumAsync(int id)
    {
        Forum? forum = _forums.FirstOrDefault(forum => forum.Id == id);

        if (forum is null)
        {
            return Task.FromResult(false);
        }

        var result = _forums.Remove(forum);

        return Task.FromResult(result);
    }
}
