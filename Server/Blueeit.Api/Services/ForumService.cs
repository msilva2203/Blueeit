using Blueeit.Api.Models;
using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.Mappings;

namespace Blueeit.Api.Services;

public interface IForumService
{
    Task<ForumResponse?> GetForumByIdAsync(int id);

    Task<int> GetCountAsync();

    int GetThreadCount(int id);

    int GetPostCount(int id);

    Task<PaginatedResult<ForumResponse>> GetAllForumsAsync(int page, int pageSize);

    Task<PaginatedResult<ForumResponse>> GetRootForumsAsync(int page, int pageSize);

    Task<PaginatedResult<ForumResponse>> GetSubforumsAsync(int id, int page, int pageSize);

    Task<ForumResponse> CreateForumAsync(int? parentId, int authorId, string title);

    Task<bool> DeleteForumAsync(int id);
}

public class InMemoryForumService : IForumService
{
    private readonly IForumThreadService _threadService;
    private readonly IPostService _postService;
    private readonly List<Forum> _forums = [];
    private int _nextId = 1;

    public InMemoryForumService(
        IForumThreadService threadService,
        IPostService postService)
    {
        _threadService = threadService;
        _postService = postService;
    }

    private ForumMetadata GetForumMetadata(Forum forum)
    {
        return new ForumMetadata
        {
            ThreadCount = GetThreadCount(forum.Id),
            PostCount  = GetPostCount(forum.Id)
        };
    }

    public Task<ForumResponse?> GetForumByIdAsync(int id)
    {
        Forum? forum = _forums.FirstOrDefault(forum => forum.Id == id);

        if (forum is null)
        {
            return Task.FromResult<ForumResponse?>(null);
        }

        var response = forum.ToResponse(GetForumMetadata(forum));

        return Task.FromResult<ForumResponse?>(response);
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(_forums.Count());
    }

    public int GetThreadCount(int id)
    {
        return _threadService.GetCountByForumId(id);
    }

    public int GetPostCount(int id)
    {
        var threadIds = _threadService
            .GetThreadsByForumId(id)
            .Select(thread => thread.Id);

        return _postService.GetCountByThreadIds(threadIds);
    }

    public Task<PaginatedResult<ForumResponse>> GetAllForumsAsync(int page, int pageSize)
    {
        var forums = _forums
            .Select(forum => forum.ToResponse(GetForumMetadata(forum)));

        int totalCount = _forums.Count();

        var items = forums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<ForumResponse>> GetRootForumsAsync(int page, int pageSize)
    {
        var forums = _forums
            .Where(forum => forum.ParentId == null)
            .Select(forum => forum.ToResponse(GetForumMetadata(forum)));

        var totalCount = forums.Count();

        var items = forums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<ForumResponse>> GetSubforumsAsync(int id, int page, int pageSize)
    {
        var subforums = _forums
            .Where(forum => forum.ParentId == id)
            .Select(forum => forum.ToResponse(GetForumMetadata(forum)));

        int totalCount = subforums.Count();

        var items = subforums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<ForumResponse> CreateForumAsync(int? parentId, int authorId, string title)
    {
        var forum = new Forum
        {
            Id = _nextId++,
            ParentId = parentId,
            AuthorId = authorId,
            CreationDate = DateTime.Now,
            Title = title
        };

        _forums.Add(forum);

        var result = forum.ToResponse(new ForumMetadata());

        return Task.FromResult(result);
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
