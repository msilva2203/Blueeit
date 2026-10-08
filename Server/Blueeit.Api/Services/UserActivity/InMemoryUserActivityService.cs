using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.UserActivity;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public class InMemoryUserActivityService : IUserActivityService
{
    private readonly List<UserActivity> _activities = [];
    private int _nextId = 1;

    public Task<UserActivityResponse?> GetByIdAsync(int id)
    {
        UserActivity? activity = _activities.FirstOrDefault(activity => activity.Id == id);

        if (activity is null)
        {
            return Task.FromResult<UserActivityResponse?>(null);
        }

        var response = activity.ToResponse();

        return Task.FromResult<UserActivityResponse?>(response);
    }

    public Task<PaginatedResult<UserActivityResponse>> GetAllAsync(int page, int pageSize)
    {
        var activities = _activities
            .Select(activity => activity.ToResponse());

        var totalCount = activities.Count();

        var items = activities
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<UserActivityResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<PaginatedResult<UserActivityResponse>> GetAllByUserIdAsync(int userId, int page, int pageSize)
    {
        var activities = _activities
            .Where(activity => activity.UserId == userId)
            .Select(activity => activity.ToResponse());

        var totalCount = activities.Count();

        var items = activities
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<UserActivityResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return Task.FromResult(result);
    }

    public Task<UserActivityResponse> CreateAsync(CreateUserActivityData data)
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

        var result = activity.ToResponse();

        return Task.FromResult(result);
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        UserActivity? activity = _activities.FirstOrDefault(activity => activity.Id == id);

        if (activity is null)
        {
            return Task.FromResult(false);
        }

        var result = _activities.Remove(activity);

        return Task.FromResult(result);
    }
}