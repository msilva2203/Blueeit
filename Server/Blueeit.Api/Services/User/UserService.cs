using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.User;

namespace Blueeit.Api.Services;

public interface IUserService
{
    Task<UserResponse?> GetUserByIdAsync(int id);

    Task<PaginatedResult<UserResponse>> GetAllUsersAsync(UserQueryKey sortKey, int page, int pageSize);

    Task<UserResponse> CreateUserAsync(string username, string email, string password);

    Task<bool> DeleteUserAsync(int id);
}