using Microsoft.AspNetCore.Mvc;

namespace BlogApi.UnitTests;

public class ControllersTests
{
    [Fact]
    public async Task GetComment_ShouldReturnOk_WhenModelExists()
    {
        CommentDto commentDto = new() { Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetCommentAsync(It.IsAny<int>()).Result)
            .Returns(commentDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetComment(1);

        Assert.IsType<CommentDto>(result.Value);
        Assert.Equal(commentDto, result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetComment_ShouldReturnNotFound_WhenModelDoesNotExist()
    {
        CommentDto commentDto = new() { Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetCommentAsync(It.IsAny<int>()).Result)
            .Returns((CommentDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetComment(1);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPost_ShouldReturnOk_WhenModelExists()
    {
        PostDto postDto = new() { Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetPostAsync(It.IsAny<int>()).Result)
            .Returns(postDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPost(1);

        Assert.IsType<PostDto>(result.Value);
        Assert.Equal(postDto, result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPost_ShouldReturnNotFound_WhenModelDoesNotExist()
    {
        PostDto postDto = new() { Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetPostAsync(It.IsAny<int>()).Result)
            .Returns((PostDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPost(1);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }
}
