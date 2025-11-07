using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet("/api/chatbox/{chatBoxId}/[controller]")]
    [Authorize] // not check authorized yet
    public async Task<IActionResult> GetMessagesByChatId(Guid chatBoxId)
    {
        try
        {
            var messages = await _messageService.GetAllMessageAsync(chatBoxId);
            return Ok(messages);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/api/v2/chatbox/{chatBoxId}/[controller]")]
    [Authorize] // not check authorized yet
    public async Task<IActionResult> GetMessagesByChatIdPagination(Guid chatBoxId, [FromQuery] PagedCriteria pagedCriteria)
    {
        try
        {
            var messages = await _messageService.GetAllMessagePaginationAsync(chatBoxId, pagedCriteria);
            return Ok(messages);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/api/v2/chatbox/{chatBoxId}/[controller]/ws")]
    [Authorize] // not check authorized yet
    public async Task GetMessagesByChatIdPaginationWebSocket(Guid chatBoxId, [FromQuery] PagedCriteria pagedCriteria)
    {
        try
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var ws = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    while (ws.State == WebSocketState.Open)
                    {
                        var messages = await _messageService.GetAllMessagePaginationAsync(chatBoxId, pagedCriteria);
                        var jsonString = JsonSerializer.Serialize(messages);
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
                Console.WriteLine($"[WebSocket] Not a WebSocket request!");
                HttpContext.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                await HttpContext.Response.WriteAsync("Only support WebSocket protocol!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WebSocket] ERROR: {ex.Message}");
            Console.WriteLine($"[WebSocket] Stack trace: {ex.StackTrace}");
            HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await HttpContext.Response.WriteAsync(ex.Message);
        }
    }

    [HttpGet("/api/chatbox/{chatBoxId}/[controller]/ws")]
    public async Task GetMessagesByChatIdWebSocket(Guid chatBoxId)
    {
        // Console.WriteLine($"[WebSocket] Incoming request for chatbox: {chatBoxId}");
        try
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                // Console.WriteLine($"[WebSocket] Valid WebSocket request, accepting connection...");
                using (var ws = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    // Console.WriteLine($"[WebSocket] Connection accepted! Starting message polling...");
                    while (ws.State == WebSocketState.Open)
                    {
                        var messages = await _messageService.GetAllMessageAsync(chatBoxId);
                        // Console.WriteLine($"[WebSocket] Fetched {messages?.Count ?? 0} messages, sending to client...");
                        var jsonString = JsonSerializer.Serialize(messages);
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostMessage([FromForm] MessageModel model)
    {
        try
        {
            var newMessage = await _messageService.SendMessageAsync(model);
            return Ok(newMessage);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }
}
