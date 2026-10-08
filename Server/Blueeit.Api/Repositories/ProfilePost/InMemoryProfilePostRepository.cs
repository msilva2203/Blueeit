using Blueeit.Api.Models;

namespace Blueeit.Api.Repositories;

public class InMemoryProfilePostRepository : IProfilePostRepository
{
    private readonly List<ProfilePost> _profilePosts = [];
    private int _nextId = 1;

    public ProfilePost? GetById(int id)
    {
        return _profilePosts.FirstOrDefault(profilePost => profilePost.Id == id);
    }

    public Task<ProfilePost?> GetByIdAsync(int id)
    {
        return Task.FromResult(GetById(id));
    }

    public IEnumerable<ProfilePost> GetAll()
    {
        return _profilePosts;
    }

    public Task<IEnumerable<ProfilePost>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<ProfilePost>>(GetAll());
    }

    public int GetCount()
    {
        return _profilePosts.Count();
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(GetCount());
    }

    public ProfilePost Create(int userId, int authorId, string content)
    {
        var profilePost = new ProfilePost
        {
            Id = _nextId++,
            UserId = userId,
            AuthorId = authorId,
            CreatedAt = DateTime.UtcNow,
            Content = content
        };

        _profilePosts.Add(profilePost);

        return profilePost;
    }

    public Task<ProfilePost> CreateAsync(int userId, int authorId, string content)
    {
        return Task.FromResult(Create(userId, authorId, content));
    }

    public bool DeleteById(int id)
    {
        ProfilePost? profilePost = _profilePosts.FirstOrDefault(profilePost => profilePost.Id == id);

        if (profilePost is null)
        {
            return false;
        }

        var result = _profilePosts.Remove(profilePost);

        return result;
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        return Task.FromResult(DeleteById(id));
    }
}