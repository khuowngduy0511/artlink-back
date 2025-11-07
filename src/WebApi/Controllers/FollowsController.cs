using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FollowsController : ControllerBase
{
    private readonly IFollowService _followService;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public FollowsController(IFollowService followService,
        IClaimService claimService,
        IMapper mapper,
        INotificationService notificationService)
    {
        _followService = followService;
        _claimService = claimService;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    // GET: /api/accounts/6/followings
    [HttpGet("/api/accounts/{followerId}/followings")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<FollowingVM>>> GetFollowings(Guid followerId)
    {
        try
        {
            var follows = await _followService.GetAllFollowingsAsync(followerId);
            return Ok(follows);
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


    // GET: api/accounts/6/followers
    [HttpGet("/api/accounts/{followingId}/followers")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<FollowerVM>>> GetFollowers(Guid followingId)
    {

        try
        {
            var follows = await _followService.GetAllFollowersAsync(followingId);
            return Ok(follows);
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

    // POST: api/follows    
    [HttpPost]
    [Authorize] // own user
    public async Task<ActionResult<Follow>> PostFollow(FollowModel model)
    {
        try
        {
            // add new follow
            await _followService.CreateFollowAsync(model);
            // add new notification
            var currentUserId = _claimService.GetCurrentUserId ?? default;
            var currentUsername = _claimService.GetCurrentUserName ?? default;
            var notification = new NotificationModel
            {
                SentToAccount = model.FollowedId,
                Content = $"Người dùng [{currentUsername}] vừa theo dõi bạn",
                NotifyType = NotifyTypeEnum.Information,
                ReferencedAccountId = currentUserId
            };
            await _notificationService.AddNotificationAsync(notification);

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }

    }

    // DELETE: api/follows
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteFollow(FollowModel model)
    {
        try
        {
            await _followService.DeleteFollowAsync(model);
            return NoContent();
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

    // GET: api/follows/is-existed
    [HttpGet("is-existed/{accountId}")]
    [Authorize]
    public async Task<IActionResult> IsFollowExisted(Guid accountId)
    {
        try
        {
            var isExisted = await _followService.IsFollowedAsync(accountId);
            return Ok(isExisted);
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
