using Blueeit.Api.Models;
using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.Mappings;

namespace Blueeit.Api.Services;

public interface IForumService
{
    Task<ForumResponse?> GetForumByIdAsync(int id);

    Task<PaginatedResult<ForumResponse>> GetAllForumsAsync(int page, int pageSize);

    Task<PaginatedResult<ForumResponse>> GetRootForumsAsync(int page, int pageSize);

    Task<PaginatedResult<ForumResponse>> GetSubforumsAsync(int id, int page, int pageSize);

    Task<ForumResponse> CreateForumAsync(int? parentId, int authorId, string title);

    Task<bool> DeleteForumAsync(int id);
}