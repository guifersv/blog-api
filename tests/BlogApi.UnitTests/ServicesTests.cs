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

    [Fact]
    public async Task CreatePost_ShouldReturnPostDto_WhenUserExists()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetUserModelAsync(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns(userModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.CreatePostModel(
                    It.Is<UserModel>(u => u == userModel),
                    It.Is<PostModel>(p =>
                        p.User == userModel
                        && p.Title == postModel.Title
                        && p.Content == postModel.Content
                        && p.CreatedAt == postModel.CreatedAt
                        && p.UpdatedAt == postModel.UpdatedAt
                    )
                ).Result
            )
            .Returns(postModel)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreatePost(userModel.Id, Utils.PostModel2Dto(postModel));

        Assert.NotNull(returnedObject);
        Assert.IsType<PostDto>(returnedObject);
        Assert.Equal(postModel.Title, returnedObject.Title);
        Assert.Equal(postModel.Content, returnedObject.Content);
        Assert.Equal(postModel.CreatedAt, returnedObject.CreatedAt);
        Assert.Equal(postModel.UpdatedAt, returnedObject.UpdatedAt);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task CreatePost_ShouldReturnNull_WhenUserDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetUserModelAsync(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns((UserModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.CreatePostModel(
                    It.Is<UserModel>(u => u == userModel),
                    It.Is<PostModel>(p =>
                        p.User == userModel
                        && p.Title == postModel.Title
                        && p.Content == postModel.Content
                        && p.CreatedAt == postModel.CreatedAt
                        && p.UpdatedAt == postModel.UpdatedAt
                    )
                ).Result
            )
            .Returns(postModel)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreatePost(userModel.Id, Utils.PostModel2Dto(postModel));

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task GetCommentAsync_ShouldReturnDto_WhenModelExists()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetCommentModelAsync(It.Is<int>(w => w == commentModel.Id)).Result)
            .Returns(commentModel)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.GetCommentAsync(commentModel.Id);

        Assert.NotNull(returnedObject);
        Assert.IsType<CommentDto>(returnedObject);
        Assert.Equal(commentModel.Content, returnedObject.Content);
        Assert.Equal(commentModel.CreatedAt, returnedObject.CreatedAt);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task GetCommentAsync_ShouldReturnNull_WhenModelDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetCommentModelAsync(It.Is<int>(w => w == commentModel.Id)).Result)
            .Returns((CommentModel?)null)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.GetCommentAsync(commentModel.Id);

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task GetPostAsync_ShouldReturnDto_WhenModelExists()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.GetPostAsync(postModel.Id);

        Assert.NotNull(returnedObject);
        Assert.IsType<PostDto>(returnedObject);
        Assert.Equal(postModel.Title, returnedObject.Title);
        Assert.Equal(postModel.Content, returnedObject.Content);
        Assert.Equal(postModel.CreatedAt, returnedObject.CreatedAt);
        Assert.Equal(postModel.UpdatedAt, returnedObject.UpdatedAt);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task GetPostAsync_ShouldReturnNull_WhenModelDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns((PostModel?)null)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.GetPostAsync(postModel.Id);

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }
}
