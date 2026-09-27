using Blueeit.Api.Common.Pagination;
using Blueeit.Api.Models;

namespace Blueeit.Api.Services;

public interface IPostService
{
    Task<Post?> GetPostByIdAsync(int id);

    Task<int> GetCountAsync();

    Task<PaginatedResult<Post>> GetAllPostsAsync(int page, int pageSize);

    Task<PaginatedResult<Post>> GetPostsByThreadId(int threadId, int page, int pageSize);

    Task<Post> CreatePostAsync(int threadId, int authorId, string content);

    Task<bool> DeletePostAsync(int id);
}

public class InMemoryPostService : IPostService
{
    private readonly List<Post> _posts = [];
    private int _nextId = 1;

    public Task<Post?> GetPostByIdAsync(int id)
    {
        Post? post = _posts.FirstOrDefault(post => post.Id == id);
        return Task.FromResult(post);
    }

    public Task<int> GetCountAsync()
    {
        var count = _posts.Count();
        return Task.FromResult(count);
    }

    public Task<PaginatedResult<Post>> GetAllPostsAsync(int page, int pageSize)
    {
        int totalCount = _posts.Count();

        IReadOnlyList<Post> posts = _posts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<Post>
        {
            Items = posts,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<Post>> GetPostsByThreadId(int threadId, int page, int pageSize)
    {
        var posts = _posts
            .Where(post => post.ThreadId == threadId);

        var totalCount = posts.Count();

        var items = posts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<Post>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<Post> CreatePostAsync(int threadId, int authorId, string content)
    {
        var post = new Post
        {
            Id = _nextId,
            ThreadId = threadId,
            AuthorId = authorId,
            CreationDate = DateTime.Now,
            Content = content
        };

        _posts.Add(post);
        _nextId++;

        return Task.FromResult(post);
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
