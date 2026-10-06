using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.Models;

namespace Blueeit.Api.Services;

public interface IForumThreadService
{
    Task<ForumThreadResponse?> GetThreadByIdAsync(int id);

    Task<int> GetCountAsync();

    int GetCountByForumId(int forumId);

    Task<PaginatedResult<ForumThreadResponse>> GetAllThreadsAsync(int page, int pageSize);

    Task<PaginatedResult<ForumThreadResponse>> GetThreadsByForumIdAsync(int forumId, int page, int pageSize);

    IEnumerable<ForumThread> GetThreadsByForumId(int forumId);

    bool BelongsToForum(int threadId, int forumId);

    Task<ForumThreadResponse> CreateThreadAsync(int forumId, int authorId, string title, string content);

    Task<bool> DeleteThreadAsync(int id);
}