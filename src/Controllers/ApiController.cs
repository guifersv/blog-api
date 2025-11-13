using System.Net.Mime;
using BlogApi.Application.Dtos;
using BlogApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
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
            _logger.LogWarning("ApiController: The Comment with id: {id} doesn't exist.", id);
            return NotFound();
        }
        else
            return commentModel;
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
            _logger.LogWarning("ApiController: The Post with id: {id} doesn't exist.", id);
            return NotFound();
        }
        else
            return postModel;
    }
}
