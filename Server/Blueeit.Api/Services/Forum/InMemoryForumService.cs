using Blueeit.Api.Models;
using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.Mappings;
using Blueeit.Api.Services.Data;
using Blueeit.Api.Repositories;

namespace Blueeit.Api.Services;

public class InMemoryForumService : IForumService
{
    private readonly IForumRepository _forumRepository;
    private readonly IForumThreadRepository _threadRepository;
    private readonly IUserActivityRepository _activityRepository;

    public InMemoryForumService(
        IForumRepository forumRepository,
        IForumThreadRepository threadRepository,
        IUserActivityRepository activityRepository)
    {
        _forumRepository = forumRepository;
        _threadRepository = threadRepository;
        _activityRepository = activityRepository;
    }

    private int GetThreadCount(int id)
    {
        return 0;
    }

    private int GetPostCount(int id)
    {
        return 0;
    }

    private ForumMetadata GetForumMetadata(Forum forum)
    {
        return new ForumMetadata
        {
            ThreadCount = GetThreadCount(forum.Id),
            PostCount  = GetPostCount(forum.Id)
        };
    }

    public async Task<ForumResponse?> GetForumByIdAsync(int id)
    {
        Forum? forum = await _forumRepository.GetByIdAsync(id);

        if (forum is null)
        {
            return null;
        }

        var response = forum.ToResponse(GetForumMetadata(forum));

        return response;
    }

    public async Task<PaginatedResult<ForumResponse>> GetAllForumsAsync(int page, int pageSize)
    {
        var all = await _forumRepository.GetAllAsync();

        var forums = all
            .Select(forum => forum.ToResponse(GetForumMetadata(forum)));

        int totalCount = forums.Count();

        var items = forums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<PaginatedResult<ForumResponse>> GetRootForumsAsync(int page, int pageSize)
    {
        var all = await _forumRepository.GetAllAsync();

        var forums = all
            .Where(forum => forum.ParentId == null)
            .Select(forum => forum.ToResponse(GetForumMetadata(forum)));

        var totalCount = forums.Count();

        var items = forums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<PaginatedResult<ForumResponse>> GetSubforumsAsync(int id, int page, int pageSize)
    {
        var all = await _forumRepository.GetAllAsync();

        var subforums = all
            .Where(forum => forum.ParentId == id)
            .Select(forum => forum.ToResponse(GetForumMetadata(forum)));

        int totalCount = subforums.Count();

        var items = subforums
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<ForumResponse> CreateForumAsync(int? parentId, int authorId, string title)
    {
        var forum = await _forumRepository.CreateAsync(
            parentId,
            authorId,
            title
        );

        var activity = await _activityRepository.CreateAsync(
            CreateUserActivityData.ForumCreated(
                forum.AuthorId,
                forum.CreatedAt,
                forum.Id
            )
        );

        var result = forum.ToResponse(new ForumMetadata());

        return result;
    }

    public async Task<bool> DeleteForumAsync(int id)
    {
        return await _forumRepository.DeleteByIdAsync(id);
    }
}