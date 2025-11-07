using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using WebApi.Utils;
namespace WebApi.Controllers;

[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    public CommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    // api/artworks/5/comments
    [Route("api/artworks/{artworkId}/comments")]
    [HttpGet]
    public async Task<IActionResult> GetCommentsOfArtwork(Guid artworkId)
    {
        try
        {
            var comments = _mapper.Map<List<CommentVM>>(
                await _commentService.GetCommentsByArtworkWithRepliesAsync(artworkId));
            return Ok(comments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // api/v2/artworks/5/comments
    [Route("api/v2/artworks/{artworkId}/comments")]
    [HttpGet]
    public async Task<IActionResult> GetCommentsOfArtworkPagination(Guid artworkId, [FromQuery] PagedCriteria pagedCriteria)
    {
        try
        {
            var comments = await _commentService
                .GetCommentsByArtworkWithRepliesPaginationAsync(artworkId, pagedCriteria);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // api/v2/artworks/5/comments
    [Route("api/v2/artworks/{artworkId}/comments/ws")]
    [HttpGet]
    public async Task GetCommentsOfArtworkPaginationWebSocket(Guid artworkId, [FromQuery] PagedCriteria pagedCriteria)
    {
        try
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var ws = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    while (ws.State == WebSocketState.Open)
                    {
                        var comments = await _commentService
                            .GetCommentsByArtworkWithRepliesPaginationAsync(artworkId, pagedCriteria);
                        var jsonString = JsonSerializer.Serialize(comments);
                        var buffer = Encoding.UTF8.GetBytes(jsonString);
                        await ws.SendAsync(
                            new ArraySegment<byte>(buffer),
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None);
                        await Task.Delay(1000);
                    }
                    // close ws connection
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure,
                        "WebSocket connection closed by Server.", CancellationToken.None);
                }
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                await HttpContext.Response.WriteAsync("Only support WebSocket protocol!");
            }
        }
        catch (Exception ex)
        {
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await HttpContext.Response.WriteAsync(ex.Message);
        }
    }

    // api/comments/5
    [Route("api/comments/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetComment(Guid id)
    {
        try
        {
            return Ok(_mapper.Map<CommentVM>(
                    await _commentService.GetCommentByIdAsync(id)));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // api/artworks/5/comments
    [Route("api/artworks/{artworkId}/comments")]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostComment(Guid artworkId, [FromBody] CommentModel model)
    {
        try
        {
            var comment = await _commentService.AddCommentAsync(artworkId, model.CommentText);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // api/artworks/5/comments
    [Route("api/comments/{id}/replies")]
    [HttpPost]
    [Authorize(Roles = "CommonUser")]
    public async Task<IActionResult> ReplyComment(Guid id, [FromBody] CommentModel model)
    {
        try
        {
            var comment = await _commentService.ReplyCommentAsync(id, model.CommentText);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // api/comments/5
    [Route("api/comments/{id}")]
    [HttpPut]
    [Authorize(Roles = "CommonUser")]
    public async Task<IActionResult> PutComment(Guid id, [FromBody] CommentModel model)
    {
        try
        {
            await _commentService.EditCommentAsync(id, model.CommentText);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // api/comments/5
    [Route("api/comments/{id}")]
    [HttpDelete]
    [Authorize(Roles = "CommonUser,Moderator,Admin")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        try
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }
}
