using Application.Models;
using Application.Services.Abstractions;
using Domain.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlocksController : ControllerBase
{
    private readonly IBlockService _blockService;
    private readonly IClaimService _claimService;

    public BlocksController(IBlockService blockService, IClaimService claimService)
    {
        _blockService = blockService;
        _claimService = claimService;
    }

    // GET: api/blocks/5/blocking
    [HttpGet("/api/accounts/{blockerId}/blockings")]
    [Authorize] // admin & own user
    public async Task<ActionResult<IEnumerable<BlockVM>>> GetBlocking(Guid blockerId)
    {
        try
        {
            var blocks = await _blockService.GetBlockingsOfBlockerAsync(blockerId);
            return Ok(blocks);
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

    // GET: api/blocks/5/blocked
    [HttpGet("/api/accounts/{blockingId}/blockers")]
    [Authorize] // admin & own user
    public async Task<IActionResult> GetBlockers(Guid blockingId)
    {
        try
        {
            var blocks = await _blockService.GetBlockersOfBlockingAsync(blockingId);
            return Ok(blocks);
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

    // POST: api/blocks
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Block>> PostBlock(BlockModel model)
    {
        try
        {
            await _blockService.CreateBlockAsync(model);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
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

    // DELETE: api/blocks/5
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteBlock(BlockModel model)
    {
        try
        {
            await _blockService.DeleteBlockAsync(model.BlockedId);
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
}
