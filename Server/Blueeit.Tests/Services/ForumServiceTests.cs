using Blueeit.Api.Services;
using Blueeit.Tests.Fixtures;

namespace Blueeit.Tests.Services;

public class ForumServiceTests : IClassFixture<BlueeitTestFixture>
{
    private readonly BlueeitTestFixture _fixture;

    public ForumServiceTests(
        BlueeitTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void  ExampleTest()
    {
        var forum = _fixture.Forums.GetById(1);

        Assert.NotNull(forum);
    }
}