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
public class ChatBoxsController : ControllerBase
{
    private readonly IChatBoxService _chatBoxService;
    private readonly IClaimService _claimService;

    public ChatBoxsController(IChatBoxService chatBoxService, IClaimService claimService)
    {
        _chatBoxService = chatBoxService;
        _claimService = claimService;
    }

    [HttpGet("/api/accounts/[controller]")]
    [Authorize]
    public async Task<IActionResult> GetChatBoxByAccountId()
    {
        try
        {
            var accountId = _claimService.GetCurrentUserId ?? default;
            var chats = await _chatBoxService.GetAllChatBoxByAccountIdAsync(accountId);
            return Ok(chats);
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

    [HttpGet("/api/accounts/{accountId}/[controller]/ws")]
    public async Task GetChatBoxByAccountIdWebSocket(Guid accountId)
    {
        try
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var ws = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    while (ws.State == WebSocketState.Open)
                    {
                        var chats = await _chatBoxService.GetAllChatBoxByAccountIdAsync(accountId);
                        var jsonString = JsonSerializer.Serialize(chats);
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

    [HttpGet("{chatboxId}")]
    public async Task<IActionResult> GetChatBoxById(Guid chatboxId)
    {
        var chat = await _chatBoxService.GetChatBoxByIdAsync(chatboxId);
        return Ok(chat);
    }
}
