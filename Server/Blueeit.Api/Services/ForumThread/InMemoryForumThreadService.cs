using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Repositories;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public class InMemoryForumThreadService : IForumThreadService
{
    private readonly IForumThreadRepository _threadRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserActivityRepository _activityRepository;

    public InMemoryForumThreadService(
        IForumThreadRepository threadRepository,
        IPostRepository postRepository,
        IUserActivityRepository activityRepository)
    {
        _threadRepository = threadRepository;
        _postRepository = postRepository;
        _activityRepository = activityRepository;
    }

    public async Task<ForumThreadResponse?> GetThreadByIdAsync(int id)
    {
        ForumThread? thread = await _threadRepository.GetByIdAsync(id);

        if (thread is null)
        {
            return null;
        }

        var response = thread.ToResponse();

        return response;
    }

    public async Task<PaginatedResult<ForumThreadResponse>> GetAllThreadsAsync(int page, int pageSize)
    {
        var all = await _threadRepository.GetAllAsync();

        var threads = all
            .Select(thread => thread.ToResponse());

        var totalCount = threads.Count();

        var items = threads
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumThreadResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<PaginatedResult<ForumThreadResponse>> GetThreadsByForumIdAsync(int forumId, int page, int pageSize)
    {
        var all = await _threadRepository.GetAllAsync();

        var threads = all
            .Where(thread => thread.ForumId == forumId)
            .Select(thread => thread.ToResponse());

        var totalCount = threads.Count();

        var items = threads
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<ForumThreadResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<ForumThreadResponse> CreateThreadAsync(int forumId, int authorId, string title, string content)
    {
        var thread = await _threadRepository.CreateAsync(
            forumId,
            authorId,
            title,
            content
        );

        var activity = await _activityRepository.CreateAsync(
            CreateUserActivityData.ThreadCreated(
                thread.AuthorId,
                thread.CreatedAt,
                thread.ForumId,
                thread.Id
            )
        );

        var post = await _postRepository.CreateAsync(
            thread.Id,
            thread.AuthorId,
            content
        );

        thread.OpeningPostId = post.Id;

        var response = thread.ToResponse();

        return response;
    }

    public async Task<bool> DeleteThreadAsync(int id)
    {
        return await _threadRepository.DeleteByIdAsync(id);
    }
}