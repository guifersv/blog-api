using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.UnitTests;

public class ControllersTests
{
    [Fact]
    public async Task GetPost_ShouldReturnOk_WhenModelExists()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetPostAsync(It.Is<int>(id => id == postDto.Id)).Result)
            .Returns(postDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPost(postDto.Id);

        Assert.IsType<PostDto>(result.Value);
        Assert.Equal(postDto, result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPost_ShouldReturnNotFound_WhenModelDoesNotExist()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetPostAsync(It.Is<int>(id => id == postDto.Id)).Result)
            .Returns((PostDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPost(postDto.Id);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPostLikes_ShouldReturnLikes_WhenPostExists()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };
        List<LikeDto> likes = [];
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetLikesFromPostAsync(It.Is<int>(id => id == postDto.Id)).Result)
            .Returns(likes)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPostLikes(postDto.Id);

        Assert.IsType<List<LikeDto>>(result.Value);
        Assert.Empty(result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPostLikes_ShouldReturnNotFound_WhenPostDoesNotExist()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetLikesFromPostAsync(It.Is<int>(id => id == postDto.Id)).Result)
            .Returns((List<LikeDto>?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPostLikes(postDto.Id);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPostComments_ShouldReturnComments_WhenPostExists()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };
        List<CommentDto> comments = [];

        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetCommentsFromPostAsync(It.Is<int>(id => id == postDto.Id)).Result)
            .Returns(comments)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPostComments(postDto.Id);

        Assert.IsType<List<CommentDto>>(result.Value);
        Assert.Empty(result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetPostComments_ShouldReturnNotFound_WhenPostDoesNotExist()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetCommentsFromPostAsync(It.Is<int>(id => id == postDto.Id)).Result)
            .Returns((List<CommentDto>?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetPostComments(postDto.Id);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetComment_ShouldReturnOk_WhenModelExists()
    {
        CommentDto commentDto = new() { Id = 1, Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetCommentAsync(It.Is<int>(id => id == commentDto.Id)).Result)
            .Returns(commentDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetComment(commentDto.Id);

        Assert.IsType<CommentDto>(result.Value);
        Assert.Equal(commentDto, result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetComment_ShouldReturnNotFound_WhenModelDoesNotExist()
    {
        CommentDto commentDto = new() { Id = 1, Content = "content" };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetCommentAsync(It.Is<int>(id => id == commentDto.Id)).Result)
            .Returns((CommentDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetComment(commentDto.Id);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetLike_ShouldReturnOk_WhenModelExists()
    {
        LikeDto likeDto = new() { Id = 1 };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetLikeAsync(It.Is<int>(id => id == likeDto.Id)).Result)
            .Returns(likeDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetLike(likeDto.Id);

        Assert.IsType<LikeDto>(result.Value);
        Assert.Equal(likeDto, result.Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task GetLike_ShouldReturnNotFound_WhenModelDoesNotExist()
    {
        LikeDto likeDto = new() { Id = 1 };
        var logger = Mock.Of<ILogger<ApiController>>();

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.GetLikeAsync(It.Is<int>(id => id == likeDto.Id)).Result)
            .Returns((LikeDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger);
        var result = await controller.GetLike(likeDto.Id);

        Assert.IsType<NotFoundResult>(result.Result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreatePost_ShouldReturnCreatedAtAction_WhenValidNameIdentifierAndServiceCreateMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };

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
                    It.Is<PostDto>(p => p == postDto)
                ).Result
            )
            .Returns(postDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreatePost(postDto);

        var resultObjet = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ApiController.GetPost), resultObjet.ActionName);
        Assert.Equal(postDto, resultObjet.Value);
        Assert.Equal(postDto.Id, resultObjet.RouteValues?.First(m => m.Key == "postId").Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreatePost_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

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
        PostDto postDto = new() { Id = 1, Content = "content" };

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
                    It.Is<PostDto>(p => p == postDto)
                ).Result
            )
            .Returns((PostDto?)null)
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
        PostDto postDto = new() { Id = 1, Content = "content" };
        LikeDto likeDto = new() { Id = 1 };

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
                    It.Is<int>(id => id == postDto.Id),
                    It.Is<LikeDto>(l => l == likeDto)
                ).Result
            )
            .Returns(likeDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateLike(postDto.Id, likeDto);

        var resultObjet = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ApiController.GetLike), resultObjet.ActionName);
        Assert.Equal(likeDto, resultObjet.Value);
        Assert.Equal(likeDto.Id, resultObjet.RouteValues?.First(m => m.Key == "likeId").Value);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateLike_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };
        LikeDto likeDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateLike(postDto.Id, likeDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateLike_ShouldReturnBadRequest_WhenServiceCreateMethodReturnsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };
        LikeDto likeDto = new() { Id = 1 };

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
                    It.Is<int>(id => id == postDto.Id),
                    It.Is<LikeDto>(l => l == likeDto)
                ).Result
            )
            .Returns((LikeDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateLike(postDto.Id, likeDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnCreatedAtAction_WhenValidNameIdentifierAndServiceCreateMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };
        CommentDto commentDto = new() { Id = 1 };

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
                    It.Is<int>(id => id == postDto.Id),
                    It.Is<CommentDto>(c => c == commentDto)
                ).Result
            )
            .Returns(commentDto)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateComment(postDto.Id, commentDto);

        var resultObjet = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ApiController.GetComment), resultObjet.ActionName);
        Assert.Equal(commentDto, resultObjet.Value);
        Assert.Equal(
            commentDto.Id,
            resultObjet.RouteValues?.First(m => m.Key == "commentId").Value
        );
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };
        CommentDto commentDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateComment(postDto.Id, commentDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnBadRequest_WhenServiceCreateMethodReturnsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };
        CommentDto commentDto = new() { Id = 1 };

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
                    It.Is<int>(id => id == postDto.Id),
                    It.Is<CommentDto>(c => c == commentDto)
                ).Result
            )
            .Returns((CommentDto?)null)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.CreateComment(postDto.Id, commentDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnNoContent_WhenValidNameIdentifierAndServiceUpdateMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };

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
                    It.Is<int>(id => id == postDto.Id),
                    It.Is<PostDto>(p => p == postDto)
                ).Result
            )
            .Returns(true)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdatePost(postDto.Id, postDto);

        Assert.IsType<NoContentResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdatePost(postDto.Id, postDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnBadRequest_WhenServiceUpdateMethodReturnsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };

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
                    It.Is<int>(id => id == postDto.Id),
                    It.Is<PostDto>(p => p == postDto)
                ).Result
            )
            .Returns(false)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdatePost(postDto.Id, postDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnNoContent_WhenValidNameIdentifierAndServiceUpdateMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };
        CommentDto commentDto = new() { Id = 1 };

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
                    It.Is<int>(id => id == commentDto.Id),
                    It.Is<CommentDto>(c => c == commentDto)
                ).Result
            )
            .Returns(true)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdateComment(commentDto.Id, commentDto);

        Assert.IsType<NoContentResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        CommentDto commentDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdateComment(commentDto.Id, commentDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnBadRequest_WhenServiceUpdateMethodReturnsNull()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };
        CommentDto commentDto = new() { Id = 1 };

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
                    It.Is<int>(id => id == commentDto.Id),
                    It.Is<CommentDto>(c => c == commentDto)
                ).Result
            )
            .Returns(false)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.UpdateComment(commentDto.Id, commentDto);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeletePost_ShouldReturnNoContent_WhenValidNameIdentifierAndServiceDeleteMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.DeletePost(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<int>(id => id == postDto.Id)
                ).Result
            )
            .Returns(true)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeletePost(postDto.Id);

        Assert.IsType<NoContentResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeletePost_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        PostDto postDto = new() { Id = 1, Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeletePost(postDto.Id);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeletePost_ShouldReturnBadRequest_WhenServiceDeleteMethodReturnsFalse()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        PostDto postDto = new() { Id = 1, Content = "content" };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.DeletePost(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<int>(id => id == postDto.Id)
                ).Result
            )
            .Returns(false)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeletePost(postDto.Id);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnNoContent_WhenValidNameIdentifierAndServiceDeleteMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        CommentDto commentDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.DeleteComment(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<int>(id => id == commentDto.Id)
                ).Result
            )
            .Returns(true)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteComment(commentDto.Id);

        Assert.IsType<NoContentResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        CommentDto commentDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteComment(commentDto.Id);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnBadRequest_WhenServiceDeleteMethodReturnsFalse()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        CommentDto commentDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.DeleteComment(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<int>(id => id == commentDto.Id)
                ).Result
            )
            .Returns(false)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteComment(commentDto.Id);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteLike_ShouldReturnNoContent_WhenValidNameIdentifierAndServiceDeleteMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        LikeDto likeDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.DeleteLike(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<int>(id => id == likeDto.Id)
                ).Result
            )
            .Returns(true)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteLike(likeDto.Id);

        Assert.IsType<NoContentResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteLike_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        LikeDto likeDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteLike(likeDto.Id);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteLike_ShouldReturnBadRequest_WhenServiceDeleteMethodReturnsFalse()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");
        LikeDto likeDto = new() { Id = 1 };

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s =>
                s.DeleteLike(
                    It.Is<string>(t => t == claim.Value),
                    It.Is<int>(id => id == likeDto.Id)
                ).Result
            )
            .Returns(false)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteLike(likeDto.Id);

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnNoContent_WhenValidNameIdentifierAndServiceDeleteMethod()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.DeleteUser(It.Is<string>(t => t == claim.Value)).Result)
            .Returns(true)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteUser();

        Assert.IsType<NoContentResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnBadRequest_WhenNameIdentifierIsNull()
    {
        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([])),
        };

        var serviceMock = new Mock<IBlogService>();

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteUser();

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnBadRequest_WhenServiceDeleteMethodReturnsFalse()
    {
        var claim = new Claim(ClaimTypes.NameIdentifier, "id");

        var logger = Mock.Of<ILogger<ApiController>>();

        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([claim])),
        };

        var serviceMock = new Mock<IBlogService>();
        serviceMock
            .Setup(s => s.DeleteUser(It.Is<string>(t => t == claim.Value)).Result)
            .Returns(false)
            .Verifiable(Times.Once());

        var controller = new ApiController(serviceMock.Object, logger)
        {
            ControllerContext = new ControllerContext { HttpContext = httpContext },
        };
        var result = await controller.DeleteUser();

        Assert.IsType<BadRequestResult>(result);
        serviceMock.Verify();
    }
}