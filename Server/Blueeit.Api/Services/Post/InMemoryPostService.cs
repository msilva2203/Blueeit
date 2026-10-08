using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.Post;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public class InMemoryPostService : IPostService
{
    private readonly IUserActivityService _activityService;
    private readonly List<Post> _posts = [];
    private int _nextId = 1;

    public InMemoryPostService(IUserActivityService activityService)
    {
        _activityService = activityService;
    }

    public Task<PostResponse?> GetPostByIdAsync(int id)
    {
        Post? post = _posts.FirstOrDefault(post => post.Id == id);

        if (post is null)
        {
            return Task.FromResult<PostResponse?>(null);
        }

        var response = post.ToResponse();

        return Task.FromResult<PostResponse?>(response);
    }

    public Task<int> GetCountAsync()
    {
        var count = _posts.Count();
        return Task.FromResult(count);
    }

    public int GetCountByAuthorId(int authorId)
    {
        return _posts.Count(post => post.AuthorId == authorId);
    }

    public int GetCountByThreadIds(IEnumerable<int> threadIds)
    {
        var ids = threadIds.ToHashSet();

        return _posts.Count(post => ids.Contains(post.ThreadId));
    }

    public Task<PaginatedResult<PostResponse>> GetAllPostsAsync(PostQueryKey queryKey, int page, int pageSize)
    {
        var posts = _posts
            .Select(post => post.ToResponse());

        int totalCount = posts.Count();

        posts = queryKey switch
        {
            PostQueryKey.None => 
                posts,

            PostQueryKey.Newest =>
                posts.OrderByDescending(post => post.CreatedAt),

            _ => posts
        };

        var items = posts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<PostResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<PostResponse>> GetPostsByThreadId(PostQueryKey queryKey, int threadId, int page, int pageSize)
    {
        var posts = _posts
            .Where(post => post.ThreadId == threadId)
            .Select(post => post.ToResponse());

        var totalCount = posts.Count();

        posts = queryKey switch
        {
            PostQueryKey.None =>
                posts,

            PostQueryKey.Newest =>
                posts.OrderByDescending(post => post.CreatedAt),

            _ => posts
        };

        var items = posts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<PostResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public async Task<PostResponse> CreatePostAsync(int threadId, int authorId, string content)
    {
        var post = new Post
        {
            Id = _nextId++,
            ThreadId = threadId,
            AuthorId = authorId,
            CreatedAt = DateTime.UtcNow,
            Content = content
        };

        _posts.Add(post);

        await _activityService.CreateAsync(
            CreateUserActivityData.PostCreated(
                post.AuthorId,
                post.CreatedAt,
                post.ThreadId,
                post.Id
            )
        );

        var response = post.ToResponse();

        return response;
    }

    public Task<bool> DeletePostAsync(int id)
    {
        Post? post = _posts.FirstOrDefault(post => post.Id == id);

        if (post is null)
        {
            return Task.FromResult(false);
        }

        bool result = _posts.Remove(post);

        return Task.FromResult(result);
    }
}