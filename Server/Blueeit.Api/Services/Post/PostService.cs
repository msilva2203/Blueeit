using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.Post;

namespace Blueeit.Api.Services;

public interface IPostService
{
    Task<PostResponse?> GetPostByIdAsync(int id);

    Task<int> GetCountAsync();

    int GetCountByAuthorId(int authorId);

    int GetCountByThreadIds(IEnumerable<int> threadIds);

    Task<PaginatedResult<PostResponse>> GetAllPostsAsync(PostQueryKey queryKey, int page, int pageSize);

    Task<PaginatedResult<PostResponse>> GetPostsByThreadId(PostQueryKey queryKey, int threadId, int page, int pageSize);

    Task<PostResponse> CreatePostAsync(int threadId, int authorId, string content);

    Task<bool> DeletePostAsync(int id);
}