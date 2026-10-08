using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.UserActivity;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Repositories;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public class InMemoryUserActivityService : IUserActivityService
{
    private readonly IUserActivityRepository _activityRepository;

    public InMemoryUserActivityService(
        IUserActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<UserActivityResponse?> GetByIdAsync(int id)
    {
        UserActivity? activity = await _activityRepository.GetByIdAsync(id);

        if (activity is null)
        {
            return null;
        }

        var result = activity.ToResponse();

        return result;
    }

    public async Task<PaginatedResult<UserActivityResponse>> GetAllAsync(int page, int pageSize)
    {
        var all = await _activityRepository.GetAllAsync();

        var activities = all
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

        return result;
    }

    public async Task<PaginatedResult<UserActivityResponse>> GetAllByUserIdAsync(int userId, int page, int pageSize)
    {
        var all = await _activityRepository.GetAllAsync();

        var activities = all
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

        return result;
    }

    public async Task<UserActivityResponse> CreateAsync(CreateUserActivityData data)
    {
        var activity = await _activityRepository.CreateAsync(data);

        var result = activity.ToResponse();

        return result;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        return await _activityRepository.DeleteByIdAsync(id);
    }
}