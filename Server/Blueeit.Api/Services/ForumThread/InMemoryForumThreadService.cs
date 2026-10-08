using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public class InMemoryForumThreadService : IForumThreadService
{
    private readonly IPostService _postService;
    private readonly IUserActivityService _activityService;
    private readonly List<ForumThread> _threads = [];
    private int _nextId = 1;

    public InMemoryForumThreadService(IPostService postService, IUserActivityService activityService)
    {
        _postService = postService;
        _activityService = activityService;
    }

    public Task<ForumThreadResponse?> GetThreadByIdAsync(int id)
    {
        ForumThread? thread = _threads.FirstOrDefault(thread => thread.Id == id);

        if (thread is null)
        {
            return Task.FromResult<ForumThreadResponse?>(null);
        }

        var response = thread.ToResponse();

        return Task.FromResult<ForumThreadResponse?>(response);
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

    public Task<PaginatedResult<ForumThreadResponse>> GetAllThreadsAsync(int page, int pageSize)
    {
        var threads = _threads
            .Select(thread => thread.ToResponse());

        var totalCount = threads.Count();

        var items = threads
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumThreadResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<ForumThreadResponse>> GetThreadsByForumIdAsync(int forumId, int page, int pageSize)
    {
        var threads = _threads
            .Where(thread => thread.ForumId == forumId)
            .Select(thread => thread.ToResponse());

        var totalCount = threads.Count();

        var items = threads
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumThreadResponse>
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

    public async Task<ForumThreadResponse> CreateThreadAsync(int forumId, int authorId, string title, string content)
    {
        var thread = new ForumThread
        {
            Id = _nextId++,
            AuthorId = authorId,
            ForumId = forumId,
            CreationDate = DateTime.Now,
            Title = title
        };

        _threads.Add(thread);

        await _activityService.CreateAsync(
            CreateUserActivityData.ThreadCreated(
                thread.AuthorId,
                thread.CreationDate,
                thread.ForumId,
                thread.Id
            )
        );

        var post = await _postService.CreatePostAsync(
            thread.Id,
            thread.AuthorId,
            content
        );

        thread.OpeningPostId = post.Id;

        var response = thread.ToResponse();

        return response;
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