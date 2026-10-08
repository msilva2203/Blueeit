using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public class InMemoryPostRepository : IPostRepository
{
    private readonly List<Post> _posts = [];
    private int _nextId = 1;

    public Post? GetById(int id)
    {
        return _posts.FirstOrDefault(post => post.Id == id);
    }

    public Task<Post?> GetByIdAsync(int id)
    {
        return Task.FromResult(GetById(id));
    }

    public IEnumerable<Post> GetAll()
    {
        return _posts;
    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Post>>(_posts);
    }

    public int GetCount()
    {
        return _posts.Count();
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(GetCount());
    }

    public Post Create(int threadId, int authorId, string content)
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

        return post;
    }

    public Task<Post> CreateAsync(int threadId, int authorId, string content)
    {
        return Task.FromResult(Create(threadId, authorId, content));
    }

    public bool DeleteById(int id)
    {
        Post? post = _posts.FirstOrDefault(post => post.Id == id);

        if (post is null)
        {
            return false;
        }

        var result = _posts.Remove(post);

        return result;
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        return Task.FromResult(DeleteById(id));
    }
}