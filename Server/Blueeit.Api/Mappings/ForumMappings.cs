using Blueeit.Api.DTOs.Forum;
using Blueeit.Api.Models;

namespace Blueeit.Api.Mappings;

public static class ForumMappings
{
    public static ForumResponse ToResponse(this Forum forum)
    {
        return new ForumResponse
        {
            Id = forum.Id,
            ParentId = forum.ParentId,
            AuthorId = forum.Id,
            CreationDate = forum.CreationDate,
            Title = forum.Title
        };
    }
}