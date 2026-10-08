using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.UserActivity;
using Blueeit.Api.Models;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public interface IUserActivityService
{
    Task<UserActivityResponse?> GetByIdAsync(int id);

    Task<PaginatedResult<UserActivityResponse>> GetAllAsync(int page, int pageSize);

    Task<PaginatedResult<UserActivityResponse>> GetAllByUserIdAsync(int userId, int page, int pageSize);

    Task<UserActivityResponse> CreateAsync(CreateUserActivityData data);

    Task<bool> DeleteByIdAsync(int id);
}