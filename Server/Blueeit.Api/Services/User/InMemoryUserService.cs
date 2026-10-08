using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.User;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public class InMemoryUserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public InMemoryUserService(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private UserMetadata GetUserMetadata(User user)
    {
        return new UserMetadata
        {
            
        };
    }

    public async Task<UserResponse?> GetUserByIdAsync(int id)
    {
        User? user = await _userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return null;
        }

        var result = user.ToResponse(GetUserMetadata(user));

        return result;
    }

    public async Task<PaginatedResult<UserResponse>> GetAllUsersAsync(UserQueryKey sortKey, int page, int pageSize)
    {
        var all = await _userRepository.GetAllAsync();

        var users = all
            .Select(user => user.ToResponse(GetUserMetadata(user)));

        int totalCount = users.Count();

        users = sortKey switch
        {
            UserQueryKey.None => 
                users,

            UserQueryKey.Newest =>
                users.OrderByDescending(user => user.CreatedAt),

            UserQueryKey.Oldest =>
                users.OrderBy(user => user.CreatedAt),

            UserQueryKey.MostMessages =>
                users.OrderByDescending(user =>
                    user.Metadata.PostCount + user.Metadata.ProfilePostCount),

            _ => users
        };

        var items = users
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<UserResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<UserResponse> CreateUserAsync(string username, string email, string password)
    {
        var user = await _userRepository.CreateAsync(
            username,
            email,
            password
        );

        var result = user.ToResponse(new UserMetadata());

        return result;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteByIdAsync(id);
    }
}