namespace BlogApi.UnitTests;

public class ServicesTests
{
    [Fact]
    public async Task CreateComment_ShouldReturnCommentDto_WhenPostAndUserExists()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new() { Post = postModel, User = userModel };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetUserModelAsync(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns(userModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.CreateCommentModel(
                    It.Is<PostModel>(p => p == postModel),
                    It.Is<CommentModel>(c => c.Post == postModel && c.User == userModel)
                ).Result
            )
            .Returns(commentModel)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreateComment(
            userModel.Id,
            postModel.Id,
            Utils.CommentModel2Dto(commentModel)
        );

        Assert.NotNull(returnedObject);
        Assert.IsType<CommentDto>(returnedObject);
        Assert.Equal(commentModel.CreatedAt, returnedObject.CreatedAt);
        Assert.Equal(commentModel.Content, returnedObject.Content);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnNull_WhenUserDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new() { Post = postModel, User = userModel };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetUserModelAsync(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns((UserModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.CreateCommentModel(
                    It.Is<PostModel>(p => p == postModel),
                    It.Is<CommentModel>(c => c.Post == postModel && c.User == userModel)
                ).Result
            )
            .Returns(commentModel)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreateComment(
            userModel.Id,
            postModel.Id,
            Utils.CommentModel2Dto(commentModel)
        );

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnNull_WhenPostDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new() { Post = postModel, User = userModel };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetUserModelAsync(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns(userModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns((PostModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.CreateCommentModel(
                    It.Is<PostModel>(p => p == postModel),
                    It.Is<CommentModel>(c => c.Post == postModel && c.User == userModel)
                ).Result
            )
            .Returns(commentModel)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreateComment(
            userModel.Id,
            postModel.Id,
            Utils.CommentModel2Dto(commentModel)
        );

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task CreateLike_ShouldReturnLikeDto_WhenPostAndUserExists()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        LikeModel likeModel = new() { Post = postModel, User = userModel };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetUserModelAsync(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns(userModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.CreateLikeModel(
                    It.Is<PostModel>(p => p == postModel),
                    It.Is<LikeModel>(c => c.Post == postModel && c.User == userModel)
                ).Result
            )
            .Returns(likeModel)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreateLike(
            userModel.Id,
            postModel.Id,
            Utils.LikeModel2Dto(likeModel)
        );

        Assert.NotNull(returnedObject);
        Assert.IsType<LikeDto>(returnedObject);
        Assert.Equal(likeModel.CreatedAt, returnedObject.CreatedAt);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task CreateLike_ShouldReturnNull_WhenUserDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        LikeModel likeModel = new() { Post = postModel, User = userModel };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetUserModelAsync(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns((UserModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.CreateLikeModel(
                    It.Is<PostModel>(p => p == postModel),
                    It.Is<LikeModel>(c => c.Post == postModel && c.User == userModel)
                ).Result
            )
            .Returns(likeModel)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreateLike(
            userModel.Id,
            postModel.Id,
            Utils.LikeModel2Dto(likeModel)
        );

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task CreateLike_ShouldReturnNull_WhenPostDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        LikeModel likeModel = new() { Post = postModel, User = userModel };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetUserModelAsync(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns(userModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns((PostModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.CreateLikeModel(
                    It.Is<PostModel>(p => p == postModel),
                    It.Is<LikeModel>(c => c.Post == postModel && c.User == userModel)
                ).Result
            )
            .Returns(likeModel)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreateLike(
            userModel.Id,
            postModel.Id,
            Utils.LikeModel2Dto(likeModel)
        );

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }
}
