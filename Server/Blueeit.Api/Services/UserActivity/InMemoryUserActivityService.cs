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
    private readonly IUserActivityFactoryService _activityFactory;

    public InMemoryUserActivityService(
        IUserActivityRepository activityRepository,
        IUserActivityFactoryService activityFactory)
    {
        _activityRepository = activityRepository;
        _activityFactory = activityFactory;
    }

    private UserActivityResponse MakeUserActivityResponse(UserActivity activity)
    {
        return _activityFactory.MakeResponse(activity);
    }

    public async Task<UserActivityResponse?> GetByIdAsync(int id)
    {
        UserActivity? activity = await _activityRepository.GetByIdAsync(id);

        if (activity is null)
        {
            return null;
        }

        var result = MakeUserActivityResponse(activity);

        return result;
    }

    public async Task<PaginatedResult<UserActivityResponse>> GetAllAsync(int page, int pageSize)
    {
        var all = await _activityRepository.GetAllAsync();

        var activities = all
            .Select(activity => MakeUserActivityResponse(activity));

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
            .Select(activity => MakeUserActivityResponse(activity));

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

        var result = MakeUserActivityResponse(activity);

        return result;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        return await _activityRepository.DeleteByIdAsync(id);
    }
}