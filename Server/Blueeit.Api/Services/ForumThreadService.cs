using Blueeit.Api.Common.Pagination;
using Blueeit.Api.Models;

namespace Blueeit.Api.Services;

public interface IForumThreadService
{
    Task<ForumThread?> GetThreadByIdAsync(int id);

    Task<int> GetCountAsync();

    Task<PaginatedResult<ForumThread>> GetAllThreadsAsync(int page, int pageSize);

    Task<PaginatedResult<ForumThread>> GetThreadsByForumIdAsync(int forumId, int page, int pageSize);

    Task<ForumThread> CreateThreadAsync(int authorId, int? forumId, string title);

    Task<bool> DeleteThreadAsync(int id);
}

public class InMemoryForumThreadService : IForumThreadService
{
    private readonly List<ForumThread> _threads = [];
    private int _nextId = 1;

    public Task<ForumThread?> GetThreadByIdAsync(int id)
    {
        ForumThread? thread = _threads.FirstOrDefault(thread => thread.Id == id);
        return Task.FromResult(thread);
    }

    public Task<int> GetCountAsync()
    {
        var count = _threads.Count();
        return Task.FromResult(count);
    }

    public Task<PaginatedResult<ForumThread>> GetAllThreadsAsync(int page, int pageSize)
    {
        var totalCount = _threads.Count();

        IReadOnlyList<ForumThread> threads = _threads
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumThread>
        {
            Items = threads,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<ForumThread>> GetThreadsByForumIdAsync(int forumId, int page, int pageSize)
    {
        var threads = _threads
            .Where(thread => thread.ForumId == forumId);

        var totalCount = threads.Count();

        var items = threads
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumThread>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<ForumThread> CreateThreadAsync(int authorId, int? forumId, string title)
    {
        var thread = new ForumThread
        {
            Id = _nextId,
            AuthorId = authorId,
            ForumId = forumId,
            CreationDate = DateTime.Now,
            Title = title
        };

        _threads.Add(thread);
        _nextId++;

        return Task.FromResult(thread);
    }

    public Task<bool> DeleteThreadAsync(int id)
    {
        ForumThread? thread = _threads.FirstOrDefault(thread => thread.Id == id);

        if (thread is null)
        {
            return Task.FromResult(false);
        }

        var result = _threads.Remove(thread);

        return Task.FromResult(result);
    }
}
