using Blueeit.Api.Models;
using Blueeit.Api.Repositories;
using Blueeit.Api.Services;

namespace Blueeit.Tests.Fixtures;

public class BlueeitTestFixture
{
    /* Repositories */
    public IForumRepository Forums { get; }
    public IForumThreadRepository Threads { get; }
    public IPostRepository Posts { get; }
    public IProfilePostRepository ProfilePosts { get; }
    public IUserActivityRepository UserActivities { get; }
    public IUserRepository Users { get; }

    /* Factories */
    public IForumFactoryService ForumFactory { get; }
    public IForumThreadFactoryService ThreadFactory { get; }
    public IPostFactoryService PostFactory { get; }
    public IProfilePostFactoryService ProfilePostFactory { get; }
    public IUserActivityFactoryService UserActivityFactory { get; }
    public IUserFactoryService UserFactory { get; }

    /* Services */
    public IForumService ForumService { get; }
    public IForumThreadService ThreadService { get; }
    public IPostService PostService { get; }
    public IProfilePostService ProfilePostService { get; }
    public IUserActivityService UserActivityService { get; }
    public IUserService UserService { get; }

    public BlueeitTestFixture()
    {
        /* Repositories */
        Forums = new InMemoryForumRepository();
        Threads = new InMemoryForumThreadRepository();
        Posts = new InMemoryPostRepository();
        ProfilePosts = new InMemoryProfilePostRepository();
        UserActivities = new InMemoryUserActivityRepository();
        Users = new InMemoryUserRepository();

        /* Factories */
        ForumFactory = new ForumFactoryService(
            Users,
            Forums,
            Threads
        );
        ThreadFactory = new ForumThreadFactoryService(
            Users,
            Forums,
            Posts
        );
        PostFactory = new PostFactoryService(
            Threads,
            Users
        );
        ProfilePostFactory = new ProfilePostFactoryService(
            Users
        );
        UserActivityFactory = new UserActivityFactoryService(
            Users,
            Forums,
            Threads,
            Posts,
            ProfilePosts
        );
        UserFactory = new UserFactoryService(
            Posts,
            ProfilePosts
        );

        /* Services */
        ForumService = new InMemoryForumService(
            Forums,
            Threads,
            UserActivities,
            ForumFactory
        );
        ThreadService = new InMemoryForumThreadService(
            Threads,
            Posts,
            UserActivities,
            ThreadFactory
        );
        PostService = new InMemoryPostService(
            Posts,
            UserActivities,
            PostFactory
        );
        ProfilePostService = new InMemoryProfilePostService(
            ProfilePosts,
            UserActivities,
            ProfilePostFactory
        );
        UserActivityService = new InMemoryUserActivityService(
            UserActivities,
            UserActivityFactory
        );
        UserService = new InMemoryUserService(
            Users,
            UserFactory
        );

        Seed();
    }

    public void Seed()
    {
        Forums.Create(null, 1, "Bug Reports");
    }
}