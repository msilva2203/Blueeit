using Blueeit.Api.Common.Pagination;
using Blueeit.Api.Models;

namespace Blueeit.Api.Services;

public interface IProfilePostService
{
    Task<ProfilePost?> GetProfilePostByIdAsync(int id);

    int GetCountByAuthorId(int authorId);

    Task<PaginatedResult<ProfilePost>> GetProfilePostsAsync(int page, int pageSize);

    Task<PaginatedResult<ProfilePost>> GetProfilePostsByUserIdAsync(int userId, int page, int pageSize);

    Task<PaginatedResult<ProfilePost>> GetProfilePostsByAuthorIdAsync(int authorId, int page, int pageSize);

    Task<ProfilePost> CreateProfilePostAsync(int userId, int authorId, string content);

    Task<bool> DeleteProfilePostAsync(int id);
}