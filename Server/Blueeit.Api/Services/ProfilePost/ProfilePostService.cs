using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ProfilePost;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.ProfilePost;

namespace Blueeit.Api.Services;

public interface IProfilePostService
{
    Task<ProfilePostResponse?> GetProfilePostByIdAsync(int id);

    Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsAsync(ProfilePostQueryKey queryKey, int page, int pageSize);

    Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsByUserIdAsync(ProfilePostQueryKey queryKey, int userId, int page, int pageSize);

    Task<PaginatedResult<ProfilePostResponse>> GetProfilePostsByAuthorIdAsync(ProfilePostQueryKey queryKey, int authorId, int page, int pageSize);

    Task<ProfilePostResponse> CreateProfilePostAsync(int userId, int authorId, string content);

    Task<bool> DeleteProfilePostAsync(int id);
}