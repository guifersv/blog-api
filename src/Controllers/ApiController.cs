using System.Net.Mime;
using System.Security.Claims;
using BlogApi.Application.Dtos;
using BlogApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class ApiController(IBlogService service, ILogger<ApiController> logger) : ControllerBase
{
    private readonly IBlogService _service = service;
    private readonly ILogger<ApiController> _logger = logger;

    [HttpGet("comment/{id}")]
    [ActionName(nameof(GetComment))]
    [EndpointSummary("Get Comment from id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CommentDto>> GetComment(int id)
    {
        _logger.LogInformation("ApiController: Retrieving Comment.");
        var commentModel = await _service.GetCommentAsync(id);

        if (commentModel is null)
        {
            _logger.LogWarning("ApiController: The Comment doesn't exist.");
            return NotFound();
        }
        else
        {
            return commentModel;
        }
    }

    [HttpGet("post/{id}")]
    [ActionName(nameof(GetPost))]
    [EndpointSummary("Get Post from id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PostDto>> GetPost(int id)
    {
        _logger.LogInformation("ApiController: Retrieving Post.");
        var postModel = await _service.GetPostAsync(id);

        if (postModel is null)
        {
            _logger.LogWarning("ApiController: The Post doesn't exist.");
            return NotFound();
        }
        else
        {
            return postModel;
        }
    }

    [HttpGet("like/{postId}")]
    [EndpointSummary("Get Likes from a Post")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<LikeDto>>> GetPostLikes(int postId)
    {
        _logger.LogInformation("ApiController: Retrieving Likes from the Post.");
        var likes = await _service.GetLikesFromPostAsync(postId);

        if (likes is null)
        {
            _logger.LogWarning("ApiController: Can't retrieve likes.");
            return NotFound();
        }
        else
        {
            return likes.ToList();
        }
    }

    [Authorize]
    [HttpPost("post")]
    [EndpointSummary("Create Post")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreatePost(PostDto post)
    {
        _logger.LogInformation("ApiController: Creating Post.");
        var nameIdentifier = HttpContext.User.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier
        );
        if (nameIdentifier is not null)
        {
            var createdModel = await _service.CreatePost(nameIdentifier.Value, post);
            if (createdModel is not null)
            {
                return CreatedAtAction(
                    nameof(GetPost),
                    new { id = createdModel.Value.Item1 },
                    createdModel.Value.Item2
                );
            }
        }
        _logger.LogWarning("ApiController: Can't create Post.");
        return BadRequest();
    }

    [Authorize]
    [HttpPost("like/{postId}")]
    [EndpointSummary("Create Like")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateLike(int postId, LikeDto like)
    {
        _logger.LogInformation("ApiController: Creating Like.");
        var nameIdentifier = HttpContext.User.Claims.FirstOrDefault(c =>
            c.Type == ClaimTypes.NameIdentifier
        );
        if (nameIdentifier is not null)
        {
            var createdModel = await _service.CreateLike(nameIdentifier.Value, postId, like);
            if (createdModel is not null)
                return Created();
        }
        _logger.LogWarning("ApiController: Can't create Like.");
        return BadRequest();
    }
}
