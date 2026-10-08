using Blueeit.Api.Models;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Repositories;

public class InMemoryUserActivityRepository : IUserActivityRepository
{
    private readonly List<UserActivity> _activities = [];
    private int _nextId = 1;

    public UserActivity? GetById(int id)
    {
        return _activities.FirstOrDefault(activity => activity.Id == id);
    }

    public Task<UserActivity?> GetByIdAsync(int id)
    {
        return Task.FromResult(GetById(id));
    }

    public IEnumerable<UserActivity> GetAll()
    {
        return _activities;
    }

    public Task<IEnumerable<UserActivity>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<UserActivity>>(GetAll());
    }

    public int GetCount()
    {
        return _activities.Count();
    }

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(GetCount());
    }

    public UserActivity Create(CreateUserActivityData data)
    {
        var activity = new UserActivity
        {
            Id = _nextId++,
            UserId = data.UserId,
            Type = data.Type,
            CreatedAt = data.CreatedAt,
            ForumId = data.ForumId,
            ThreadId = data.ThreadId,
            PostId = data.PostId,
            ProfilePostId = data.ProfilePostId,
            ProfileOwnerId = data.ProfileOwnerId
        };

        _activities.Add(activity);

        return activity;
    }

    public Task<UserActivity> CreateAsync(CreateUserActivityData data)
    {
        return Task.FromResult(Create(data));
    }

    public bool DeleteById(int id)
    {
        UserActivity? activity = _activities.FirstOrDefault(activity => activity.Id == id);

        if (activity is null)
        {
            return false;
        }

        var result = _activities.Remove(activity);

        return result;
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        return Task.FromResult(DeleteById(id));
    }
}