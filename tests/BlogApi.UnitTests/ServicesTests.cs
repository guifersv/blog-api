namespace BlogApi.UnitTests;

public class ServicesTests
{
    [Fact]
    public async Task CreatePost_ShouldReturnTupleWithIdAndDto_WhenUserExists()
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
            .Returns<UserModel, PostModel>((_, p) => p)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreatePost(userModel.Id, Utils.PostModel2Dto(postModel));

        Assert.NotNull(returnedObject);
        Assert.IsType<ValueTuple<int, PostDto>>(returnedObject);
        var model = returnedObject.Value.Item2;
        Assert.Equal(postModel.Title, model.Title);
        Assert.Equal(postModel.Content, model.Content);
        Assert.Equal(postModel.CreatedAt, model.CreatedAt);
        Assert.Equal(postModel.UpdatedAt, model.UpdatedAt);
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
            .Returns<UserModel, PostModel>((_, p) => p)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreatePost(userModel.Id, Utils.PostModel2Dto(postModel));

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task CreateComment_ShouldReturnTupleWithIdAndDto_WhenPostAndUserExists()
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
                    It.Is<CommentModel>(c =>
                        c.Post == postModel
                        && c.User == userModel
                        && c.Content == commentModel.Content
                        && c.CreatedAt == commentModel.CreatedAt
                    )
                ).Result
            )
            .Returns<PostModel, CommentModel>((_, c) => c)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreateComment(
            userModel.Id,
            postModel.Id,
            Utils.CommentModel2Dto(commentModel)
        );

        Assert.NotNull(returnedObject);
        Assert.IsType<ValueTuple<int, CommentDto>>(returnedObject);
        var model = returnedObject.Value.Item2;
        Assert.Equal(commentModel.CreatedAt, model.CreatedAt);
        Assert.Equal(commentModel.Content, model.Content);
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
                    It.Is<CommentModel>(c =>
                        c.Post == postModel
                        && c.User == userModel
                        && c.Content == commentModel.Content
                        && c.CreatedAt == commentModel.CreatedAt
                    )
                ).Result
            )
            .Returns<PostModel, CommentModel>((_, c) => c)
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
                    It.Is<CommentModel>(c =>
                        c.Post == postModel
                        && c.User == userModel
                        && c.Content == commentModel.Content
                        && c.CreatedAt == commentModel.CreatedAt
                    )
                ).Result
            )
            .Returns<PostModel, CommentModel>((_, c) => c)
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
    public async Task CreateLike_ShouldReturnTupleWithIdAndDto_WhenPostAndUserExists()
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
                    It.Is<LikeModel>(c =>
                        c.Post == postModel
                        && c.User == userModel
                        && c.CreatedAt == likeModel.CreatedAt
                    )
                ).Result
            )
            .Returns<PostModel, LikeModel>((_, c) => c)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.CreateLike(
            userModel.Id,
            postModel.Id,
            Utils.LikeModel2Dto(likeModel)
        );

        Assert.NotNull(returnedObject);
        Assert.IsType<ValueTuple<int, LikeDto>>(returnedObject);
        var model = returnedObject.Value.Item2;
        Assert.Equal(likeModel.CreatedAt, model.CreatedAt);
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
                    It.Is<LikeModel>(c =>
                        c.Post == postModel
                        && c.User == userModel
                        && c.CreatedAt == likeModel.CreatedAt
                    )
                ).Result
            )
            .Returns<PostModel, LikeModel>((_, c) => c)
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
                    It.Is<LikeModel>(c =>
                        c.Post == postModel
                        && c.User == userModel
                        && c.CreatedAt == likeModel.CreatedAt
                    )
                ).Result
            )
            .Returns<PostModel, LikeModel>((_, c) => c)
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
    public async Task GetCommentsFromPostAsync_ShouldReturnCommentsDto_WhenPostExists()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
        };

        postModel.CommentModelNavigation = [commentModel];

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.GetCommentsFromPostAsync(postModel.Id);

        Assert.NotNull(returnedObject);
        var element = Assert.Single(returnedObject);
        Assert.IsType<CommentDto>(element);
        Assert.Equal(commentModel.Content, element.Content);
        Assert.Equal(commentModel.CreatedAt, element.CreatedAt);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task GetCommentsFromPostAsync_ShouldReturnNull_WhenPostDoesNotExist()
    {
        UserModel userModel = new();
        PostModel postModel = new() { User = userModel };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns((PostModel?)null)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.GetCommentsFromPostAsync(postModel.Id);

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task GetLikesFromPostAsync_ShouldReturnLikesDto_WhenPostExists()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        LikeModel likeModel = new() { User = userModel, Post = postModel };

        postModel.LikeModelNavigation = [likeModel];

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.GetLikesFromPostAsync(postModel.Id);

        Assert.NotNull(returnedObject);
        var element = Assert.Single(returnedObject);
        Assert.IsType<LikeDto>(element);
        Assert.Equal(likeModel.CreatedAt, element.CreatedAt);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task GetLikesFromPostAsync_ShouldReturnNull_WhenPostDoesNotExist()
    {
        UserModel userModel = new();
        PostModel postModel = new() { User = userModel, Id = 1 };
        LikeModel likeModel = new() { User = userModel, Post = postModel };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.GetPostModelAsync(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns((PostModel?)null)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.GetLikesFromPostAsync(postModel.Id);

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnDto_WhenModelExistsAndSameUser()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new()
        {
            User = userModel,
            Id = 1,
            Title = "title",
        };
        PostModel updatedPostModel = new()
        {
            User = userModel,
            Id = 1,
            Title = "alias",
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindPostModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.UpdatePostModel(
                    It.Is<PostModel>(w =>
                        w.Title == updatedPostModel.Title
                        && w.Content == updatedPostModel.Content
                        && w.CreatedAt == updatedPostModel.CreatedAt
                        && w.UpdatedAt == updatedPostModel.UpdatedAt
                    )
                ).Result
            )
            .Returns<PostModel>(p => p)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.UpdatePost(
            userModel.Id,
            postModel.Id,
            Utils.PostModel2Dto(updatedPostModel)
        );

        Assert.NotNull(returnedObject);
        Assert.IsType<PostDto>(returnedObject);
        Assert.Equal(updatedPostModel.Title, returnedObject.Title);
        Assert.Equal(updatedPostModel.Content, returnedObject.Content);
        Assert.Equal(updatedPostModel.CreatedAt, returnedObject.CreatedAt);
        Assert.Equal(updatedPostModel.UpdatedAt, returnedObject.UpdatedAt);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnNull_WhenUserDiffer()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new()
        {
            User = userModel,
            Id = 1,
            Title = "title",
        };
        PostModel updatedPostModel = new()
        {
            User = userModel,
            Id = 1,
            Title = "alias",
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindPostModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.UpdatePostModel(
                    It.Is<PostModel>(w =>
                        w.Title == updatedPostModel.Title
                        && w.Content == updatedPostModel.Content
                        && w.CreatedAt == updatedPostModel.CreatedAt
                        && w.UpdatedAt == updatedPostModel.UpdatedAt
                    )
                ).Result
            )
            .Returns<PostModel>(p => p)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.UpdatePost(
            "id2",
            postModel.Id,
            Utils.PostModel2Dto(updatedPostModel)
        );

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task UpdatePost_ShouldReturnNull_WhenModelDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new()
        {
            User = userModel,
            Id = 1,
            Title = "title",
        };
        PostModel updatedPostModel = new()
        {
            User = userModel,
            Id = 1,
            Title = "alias",
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindPostModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns((PostModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.UpdatePostModel(
                    It.Is<PostModel>(w =>
                        w.Title == updatedPostModel.Title
                        && w.Content == updatedPostModel.Content
                        && w.CreatedAt == updatedPostModel.CreatedAt
                        && w.UpdatedAt == updatedPostModel.UpdatedAt
                    )
                ).Result
            )
            .Returns<PostModel>(p => p)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.UpdatePost(
            userModel.Id,
            postModel.Id,
            Utils.PostModel2Dto(updatedPostModel)
        );

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnDto_WhenModelExistsAndSameUser()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
            Content = "content",
        };
        CommentModel updatedCommentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
            Content = "update",
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindCommentModelById(It.Is<int>(w => w == commentModel.Id)).Result)
            .Returns(commentModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.UpdateCommentModel(
                    It.Is<CommentModel>(w =>
                        w.Content == updatedCommentModel.Content
                        && w.CreatedAt == updatedCommentModel.CreatedAt
                    )
                ).Result
            )
            .Returns<CommentModel>(p => p)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.UpdateComment(
            userModel.Id,
            commentModel.Id,
            Utils.CommentModel2Dto(updatedCommentModel)
        );

        Assert.NotNull(returnedObject);
        Assert.IsType<CommentDto>(returnedObject);
        Assert.Equal(updatedCommentModel.Content, returnedObject.Content);
        Assert.Equal(updatedCommentModel.CreatedAt, returnedObject.CreatedAt);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnNull_WhenUserDiffer()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
            Content = "content",
        };
        CommentModel updatedCommentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
            Content = "update",
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindCommentModelById(It.Is<int>(w => w == commentModel.Id)).Result)
            .Returns(commentModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.UpdateCommentModel(
                    It.Is<CommentModel>(w =>
                        w.Content == updatedCommentModel.Content
                        && w.CreatedAt == updatedCommentModel.CreatedAt
                    )
                ).Result
            )
            .Returns<CommentModel>(p => p)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.UpdateComment(
            "id2",
            commentModel.Id,
            Utils.CommentModel2Dto(updatedCommentModel)
        );

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task UpdateComment_ShouldReturnNull_WhenModelDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        CommentModel commentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
            Content = "content",
        };
        CommentModel updatedCommentModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
            Content = "update",
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindCommentModelById(It.Is<int>(w => w == commentModel.Id)).Result)
            .Returns((CommentModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r =>
                r.UpdateCommentModel(
                    It.Is<CommentModel>(w =>
                        w.Content == updatedCommentModel.Content
                        && w.CreatedAt == updatedCommentModel.CreatedAt
                    )
                ).Result
            )
            .Returns<CommentModel>(p => p)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var returnedObject = await service.UpdateComment(
            userModel.Id,
            commentModel.Id,
            Utils.CommentModel2Dto(updatedCommentModel)
        );

        Assert.Null(returnedObject);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeletePost_ShouldReturnTrue_WhenModelExistsAndSameUser()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindPostModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeletePostModel(It.Is<PostModel>(w => w == postModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeletePost(userModel.Id, postModel.Id);

        Assert.True(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeletePost_ShouldReturnFalse_WhenUserDiffer()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindPostModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(postModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeletePostModel(It.Is<PostModel>(w => w == postModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeletePost("id2", postModel.Id);

        Assert.False(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeletePost_ShouldReturnFalse_WhenModelDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindPostModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns((PostModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeletePostModel(It.Is<PostModel>(w => w == postModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeletePost(userModel.Id, postModel.Id);

        Assert.False(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnTrue_WhenModelExistsAndSameUser()
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
            .Setup(r => r.FindCommentModelById(It.Is<int>(w => w == commentModel.Id)).Result)
            .Returns(commentModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeleteCommentModel(It.Is<CommentModel>(w => w == commentModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeleteComment(userModel.Id, commentModel.Id);

        Assert.True(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnFalse_WhenUserDiffer()
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
            .Setup(r => r.FindCommentModelById(It.Is<int>(w => w == commentModel.Id)).Result)
            .Returns(commentModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeleteCommentModel(It.Is<CommentModel>(w => w == commentModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeleteComment("id2", commentModel.Id);

        Assert.False(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeleteComment_ShouldReturnFalse_WhenModelDoesNotExist()
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
            .Setup(r => r.FindCommentModelById(It.Is<int>(w => w == commentModel.Id)).Result)
            .Returns((CommentModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeleteCommentModel(It.Is<CommentModel>(w => w == commentModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeleteComment(userModel.Id, commentModel.Id);

        Assert.False(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeleteLike_ShouldReturnTrue_WhenModelExistsAndSameUser()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        LikeModel likeModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindLikeModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(likeModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeleteLikeModel(It.Is<LikeModel>(w => w == likeModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeleteLike(userModel.Id, likeModel.Id);

        Assert.True(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeleteLike_ShouldReturnFalse_WhenUserDiffer()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        LikeModel likeModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindLikeModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns(likeModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeleteLikeModel(It.Is<LikeModel>(w => w == likeModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeleteLike("id2", likeModel.Id);

        Assert.False(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeleteLike_ShouldReturnFalse_WhenModelDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };
        PostModel postModel = new() { User = userModel, Id = 1 };
        LikeModel likeModel = new()
        {
            Id = 1,
            Post = postModel,
            User = userModel,
        };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindLikeModelById(It.Is<int>(w => w == postModel.Id)).Result)
            .Returns((LikeModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeleteLikeModel(It.Is<LikeModel>(w => w == likeModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeleteLike(userModel.Id, likeModel.Id);

        Assert.False(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnTrue_WhenModelExists()
    {
        UserModel userModel = new() { Id = "id" };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindUserModelById(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns(userModel)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeleteUserModel(It.Is<UserModel>(w => w == userModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Once());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeleteUser(userModel.Id);

        Assert.True(result);
        repositoryMock.Verify();
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnFalse_WhenModelDoesNotExist()
    {
        UserModel userModel = new() { Id = "id" };

        var logger = Mock.Of<ILogger<BlogService>>();

        var repositoryMock = new Mock<IBlogRepository>();
        repositoryMock
            .Setup(r => r.FindUserModelById(It.Is<string>(w => w == userModel.Id)).Result)
            .Returns((UserModel?)null)
            .Verifiable(Times.Once());
        repositoryMock
            .Setup(r => r.DeleteUserModel(It.Is<UserModel>(w => w == userModel)))
            .Returns(Task.CompletedTask)
            .Verifiable(Times.Never());

        var service = new BlogService(logger, repositoryMock.Object);
        var result = await service.DeleteUser(userModel.Id);

        Assert.False(result);
        repositoryMock.Verify();
    }
}
