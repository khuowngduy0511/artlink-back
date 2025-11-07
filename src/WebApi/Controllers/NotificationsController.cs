using Application.Filters;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IClaimService _claimService;

    public NotificationsController(INotificationService notificationService,
        IClaimService claimService)
    {
        _notificationService = notificationService;
        _claimService = claimService;
    }

    [HttpGet("/api/accounts/{accountId}/[controller]/ws")]
    public async Task GetNotificationOfCurrentAccountWebSocket(Guid accountId, [FromQuery] PagedCriteria pagedCriteria)
    {
        try
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using (var ws = await HttpContext.WebSockets.AcceptWebSocketAsync())
                {
                    while (ws.State == WebSocketState.Open)
                    {
                        //var currentUser = _claimService.GetCurrentUserId ?? default;
                        var chats = await _notificationService
                            .GetNotificationsOfAnAccountAsync(accountId, pagedCriteria);
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
}
