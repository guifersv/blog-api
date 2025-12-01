using System.Security.Claims;
using Microsoft.AspNetCore.Http;
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

    [Fact]
    public async Task CreatePost_ShouldReturnCreatedAtAction_WhenNameIdentifierAndUserExists()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreatePost(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<PostDto>(p =>
                        p.Title == postDto.Title
                        && p.Content == postDto.Content
                        && p.CreatedAt == postDto.CreatedAt
                        && p.UpdatedAt == postDto.UpdatedAt
                    )
                ).Result
            )
            .Returns((1, postDto))
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreatePost(postDto);

        var resultObjet = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ApiController.GetPost), resultObjet.ActionName);
        Assert.Equal(postDto, resultObjet.Value);
        Assert.Equal(1, resultObjet.RouteValues?.First(m => m.Key == "id").Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreatePost_ShouldReturnBadRequest_WhenNameIdentifierNotExist()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity()),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreatePost(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<PostDto>(p =>
                        p.Title == postDto.Title
                        && p.Content == postDto.Content
                        && p.CreatedAt == postDto.CreatedAt
                        && p.UpdatedAt == postDto.UpdatedAt
                    )
                ).Result
            )
            .Returns((1, postDto))
            .Verifiable(Times.Never());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreatePost(postDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreatePost_ShouldReturnBadRequest_WhenUserDoesNotExist()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreatePost(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<PostDto>(p =>
                        p.Title == postDto.Title
                        && p.Content == postDto.Content
                        && p.CreatedAt == postDto.CreatedAt
                        && p.UpdatedAt == postDto.UpdatedAt
                    )
                ).Result
            )
            .Returns((ValueTuple<int, PostDto>?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreatePost(postDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }
}
