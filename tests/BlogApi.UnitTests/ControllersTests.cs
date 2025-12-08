using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.UnitTests;

public class ControllersTests
{
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
    public async Task GetPostLikes_ShouldReturnLikes_WhenPostExists()
    {
        PostDto postDto = new() { Content = "content" };
        List<LikeDto> likes = [];
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetLikesFromPostAsync(It.IsAny<int>()).Result)
            .Returns(likes)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPostLikes(1);

        Assert.IsType<List<LikeDto>>(result.Value);
        Assert.Empty(result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPostLikes_ShouldReturnNotFound_WhenPostDoesNotExist()
    {
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetLikesFromPostAsync(It.IsAny<int>()).Result)
            .Returns((List<LikeDto>?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPostLikes(1);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPostComments_ShouldReturnComments_WhenPostExists()
    {
        List<CommentDto> comments = [];
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetCommentsFromPostAsync(It.IsAny<int>()).Result)
            .Returns(comments)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPostComments(1);

        Assert.IsType<List<CommentDto>>(result.Value);
        Assert.Empty(result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPostComments_ShouldReturnNotFound_WhenPostDoesNotExist()
    {
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetCommentsFromPostAsync(It.IsAny<int>()).Result)
            .Returns((List<CommentDto>?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPostComments(1);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }

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
    public async Task CreatePost_ShouldReturnCreatedAtAction_WhenValidNameIdentifierAndServiceCreateMethod()
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
        Assert.Equal(1, resultObjet.RouteValues?.First(m => m.Key == "postId").Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreatePost_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
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
    public async Task CreatePost_ShouldReturnBadRequest_WhenServiceCreateMethodReturnsNull()
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

    [Fact]
    public async Task CreateLike_ShouldReturnCreatedAtAction_WhenValidNameIdentifierAndServiceCreateMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        LikeDto likeDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreateLike(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<LikeDto>(p => p.CreatedAt == postDto.CreatedAt)
                ).Result
            )
            .Returns((1, likeDto))
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateLike(1, likeDto);

        var resultObjet = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ApiController.GetLike), resultObjet.ActionName);
        Assert.Equal(likeDto, resultObjet.Value);
        Assert.Equal(1, resultObjet.RouteValues?.First(m => m.Key == "likeId").Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateLike_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        LikeDto likeDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity()),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreateLike(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<LikeDto>(p => p.CreatedAt == postDto.CreatedAt)
                ).Result
            )
            .Returns((1, likeDto))
            .Verifiable(Times.Never());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateLike(1, likeDto);

        var resultObjet = Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateLike_ShouldReturnBadRequest_WhenServiceCreateMethodReturnsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        LikeDto likeDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreateLike(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<LikeDto>(p => p.CreatedAt == postDto.CreatedAt)
                ).Result
            )
            .Returns((ValueTuple<int, LikeDto>?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateLike(1, likeDto);

        var resultObjet = Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnCreatedAtAction_WhenValidNameIdentifierAndServiceCreateMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        CommentDto commentDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreateComment(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<CommentDto>(c =>
                        c.CreatedAt == commentDto.CreatedAt && c.Content == commentDto.Content
                    )
                ).Result
            )
            .Returns((1, commentDto))
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateComment(1, commentDto);

        var resultObjet = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ApiController.GetComment), resultObjet.ActionName);
        Assert.Equal(commentDto, resultObjet.Value);
        Assert.Equal(1, resultObjet.RouteValues?.First(m => m.Key == "commentId").Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        CommentDto commentDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreateComment(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<CommentDto>(c =>
                        c.CreatedAt == commentDto.CreatedAt && c.Content == commentDto.Content
                    )
                ).Result
            )
            .Returns((1, commentDto))
            .Verifiable(Times.Never());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateComment(1, commentDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnBadRequest_WhenServiceCreateMethodReturnsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        CommentDto commentDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.CreateComment(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<CommentDto>(c =>
                        c.CreatedAt == commentDto.CreatedAt && c.Content == commentDto.Content
                    )
                ).Result
            )
            .Returns((ValueTuple<int, CommentDto>?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateComment(1, commentDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnNoContent_WhenValidNameIdentifierAndServiceUpdateMethod()
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
                s.UpdatePost(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<PostDto>(p =>
                        p.Title == postDto.Title
                        && p.Content == postDto.Content
                        && p.CreatedAt == postDto.CreatedAt
                        && p.UpdatedAt == postDto.UpdatedAt
                    )
                ).Result
            )
            .Returns(postDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdatePost(1, postDto);

        Assert.IsType<NoContentResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
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
                s.UpdatePost(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<PostDto>(p =>
                        p.Title == postDto.Title
                        && p.Content == postDto.Content
                        && p.CreatedAt == postDto.CreatedAt
                        && p.UpdatedAt == postDto.UpdatedAt
                    )
                ).Result
            )
            .Returns(postDto)
            .Verifiable(Times.Never());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdatePost(1, postDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnBadRequest_WhenServiceUpdateMethodReturnsNull()
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
                s.UpdatePost(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<PostDto>(p =>
                        p.Title == postDto.Title
                        && p.Content == postDto.Content
                        && p.CreatedAt == postDto.CreatedAt
                        && p.UpdatedAt == postDto.UpdatedAt
                    )
                ).Result
            )
            .Returns((PostDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdatePost(1, postDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnNoContent_WhenValidNameIdentifierAndServiceUpdateMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        CommentDto commentDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.UpdateComment(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<CommentDto>(c =>
                        c.CreatedAt == commentDto.CreatedAt && c.Content == commentDto.Content
                    )
                ).Result
            )
            .Returns(commentDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdateComment(1, commentDto);

        Assert.IsType<NoContentResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        CommentDto commentDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity()),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.UpdateComment(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<CommentDto>(c =>
                        c.CreatedAt == commentDto.CreatedAt && c.Content == commentDto.Content
                    )
                ).Result
            )
            .Returns(commentDto)
            .Verifiable(Times.Never());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdateComment(1, commentDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnBadRequest_WhenServiceUpdateMethodReturnsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Content = "content" };
        CommentDto commentDto = new();

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.UpdateComment(
                    It.Is<string>(t => t == claim.Value),
                    It.IsAny<int>(),
                    It.Is<CommentDto>(c =>
                        c.CreatedAt == commentDto.CreatedAt && c.Content == commentDto.Content
                    )
                ).Result
            )
            .Returns((CommentDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdateComment(1, commentDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }
}
