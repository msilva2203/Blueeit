using Blueeit.Api.Common.Pagination;
using Blueeit.Api.DTOs.ForumThread;
using Blueeit.Api.DTOs.Post;
using Blueeit.Api.DTOs.User;
using Blueeit.Api.Mappings;
using Blueeit.Api.Models;
using Blueeit.Api.Queries.Post;
using Blueeit.Api.Repositories;
using Blueeit.Api.Services.Data;

namespace Blueeit.Api.Services;

public class InMemoryPostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserActivityRepository _activityRepository;

    public InMemoryPostService(
        IPostRepository postRepository,
        IUserActivityRepository activityRepository)
    {
        _postRepository = postRepository;
        _activityRepository = activityRepository;
    }

    private PostResponse GetPostResponse(Post post)
    {
        var thread = new ForumThreadSummary
        {
            Title = ""
        };

        var author = new UserSummary
        {
            Username = "msilva",
            Email = "pff"
        };

        return post.ToResponse(
            thread,
            author
        );
    }

    public async Task<PostResponse?> GetPostByIdAsync(int id)
    {
        Post? post = await _postRepository.GetByIdAsync(id);

        if (post is null)
        {
            return null;
        }

        var result = GetPostResponse(post);

        return result;
    }

    public int GetCountByThreadIds(IEnumerable<int> threadIds)
    {
        var ids = threadIds.ToHashSet();

        var all = _postRepository.GetAll();

        return all.Count(post => ids.Contains(post.ThreadId));
    }

    public async Task<PaginatedResult<PostResponse>> GetAllPostsAsync(PostQueryKey queryKey, int page, int pageSize)
    {
        var all = await _postRepository.GetAllAsync();

        var posts = all
            .Select(post => GetPostResponse(post));

        int totalCount = posts.Count();

        posts = queryKey switch
        {
            PostQueryKey.None => 
                posts,

            PostQueryKey.Newest =>
                posts.OrderByDescending(post => post.CreatedAt),

            _ => posts
        };

        var items = posts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<PostResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<PaginatedResult<PostResponse>> GetPostsByThreadId(PostQueryKey queryKey, int threadId, int page, int pageSize)
    {
        var all = await _postRepository.GetAllAsync();

        var posts = all
            .Where(post => post.ThreadId == threadId)
            .Select(post => GetPostResponse(post));

        var totalCount = posts.Count();

        posts = queryKey switch
        {
            PostQueryKey.None =>
                posts,

            PostQueryKey.Newest =>
                posts.OrderByDescending(post => post.CreatedAt),

            _ => posts
        };

        var items = posts
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new PaginatedResult<PostResponse>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        return result;
    }

    public async Task<PostResponse> CreatePostAsync(int threadId, int authorId, string content)
    {
        var post = await _postRepository.CreateAsync(
            threadId,
            authorId,
            content
        );

        var activity = await _activityRepository.CreateAsync(
            CreateUserActivityData.PostCreated(
                post.AuthorId,
                post.CreatedAt,
                post.ThreadId,
                post.Id
            )
        );

        var response = GetPostResponse(post);

        return response;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        return await _postRepository.DeleteByIdAsync(id);
    }
}