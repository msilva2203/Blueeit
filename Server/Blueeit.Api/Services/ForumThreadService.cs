using Blueeit.Api.Common.Pagination;
using Blueeit.Api.Models;

namespace Blueeit.Api.Services;

public interface IForumThreadService
{
    Task<ForumThread?> GetThreadByIdAsync(int id);

    Task<int> GetCountAsync();

    int GetCountByForumId(int forumId);

    Task<PaginatedResult<ForumThread>> GetAllThreadsAsync(int page, int pageSize);

    Task<PaginatedResult<ForumThread>> GetThreadsByForumIdAsync(int forumId, int page, int pageSize);

    IEnumerable<ForumThread> GetThreadsByForumId(int forumId);

    bool BelongsToForum(int threadId, int forumId);

    Task<ForumThread> CreateThreadAsync(int authorId, int? forumId, string title, string content);

    Task<bool> DeleteThreadAsync(int id);
}

public class InMemoryForumThreadService : IForumThreadService
{
    private readonly IPostService _postService;
    private readonly List<ForumThread> _threads = [];
    private int _nextId = 1;

    public InMemoryForumThreadService(IPostService postService)
    {
        _postService = postService;
    }

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

    public int GetCountByForumId(int forumId)
    {
        var count = _threads.Count(thread => thread.ForumId == forumId);
        return count;
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

    public IEnumerable<ForumThread> GetThreadsByForumId(int forumId)
    {
        return _threads
            .Where(thread => thread.ForumId == forumId);
    }

    public bool BelongsToForum(int threadId, int forumId)
    {
        return _threads.Any(thread => thread.Id == threadId && thread.ForumId == forumId);
    }

    public async Task<ForumThread> CreateThreadAsync(int authorId, int? forumId, string title, string content)
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

        var post = await _postService.CreatePostAsync(
            thread.Id,
            thread.AuthorId,
            content
        );

        thread.OpeningPostId = post.Id;

        return thread;
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
