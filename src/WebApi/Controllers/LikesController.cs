using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likeService;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;

    public LikesController(ILikeService likeService,
        IClaimService claimService,
        IMapper mapper)
    {
        _likeService = likeService;
        _claimService = claimService;
        _mapper = mapper;
    }

    // GET: api/likes/5/artworks
    [HttpGet("/api/artworks/{artworkId}/likes")]
    public async Task<IActionResult> GetAllLikesOfArtwork(Guid artworkId)
    {
        try
        {
            var result = await _likeService.GetAllLikesOfArtworkAsync(artworkId);
            return Ok(result);
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

    // GET: api/likes/5/accounts
    [HttpGet("/api/accounts/{accountId}/likes")]
    [Authorize]
    public async Task<IActionResult> GetAllLikesOfAccount(Guid accountId)
    {
        try
        {
            var result = await _likeService.GetAllLikesOfAccountAsync(accountId);
            return Ok(result);
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

    // GET: api/likes/artworks/1
    [HttpGet("artworks/{artworkId}")]
    [Authorize]
    public async Task<IActionResult> GetIsLikedOfArtwork(Guid artworkId)
    {
        try
        {
            Guid accountId = _claimService.GetCurrentUserId ?? default;
            var result = await _likeService.GetIsLikedArtworkAsync(accountId, artworkId);
            return Ok(result);
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

    // GET: api/likes/artworks/1
    [HttpGet("artworks")]
    [Authorize]
    public async Task<IActionResult> GetIsLikedOfArtwork([FromQuery] List<Guid> artworkIds)
    {
        try
        {
            Guid accountId = _claimService.GetCurrentUserId ?? default;
            var result = await _likeService.GetListIsLikedArtworkAsync(accountId, artworkIds);
            return Ok(result);
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

    // POST: api/likes
    [HttpPost]
    [Authorize] // admin & own user
    public async Task<IActionResult> AddLike(LikeModel model)
    {
        try
        {
            await _likeService.CreateLikeAsync(model);
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

    // DELETE: api/likes
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteLike(LikeModel model)
    {
        try
        {
            Console.WriteLine($"[DeleteLike] Received request to unlike artwork: {model.ArtworkId}");
            await _likeService.DeleteLikeAsync(model.ArtworkId);
            Console.WriteLine($"[DeleteLike] Successfully unliked artwork: {model.ArtworkId}");
            return NoContent();

        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"[DeleteLike] KeyNotFoundException: {ex.Message}");
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (BadHttpRequestException ex)
        {
            Console.WriteLine($"[DeleteLike] BadHttpRequestException: {ex.Message}");
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DeleteLike] Exception: {ex.Message} | StackTrace: {ex.StackTrace}");
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }
}
