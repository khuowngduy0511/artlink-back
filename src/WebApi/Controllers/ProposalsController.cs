using Application.Models;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProposalsController : ControllerBase
{
    private readonly IProposalService _proposalService;
    private readonly IMilestoneService _milestoneService;
    private readonly IReviewService _reviewService;

    public ProposalsController(
        IProposalService proposalService,
        IMilestoneService milestoneService,
        IReviewService reviewService)
    {
        _proposalService = proposalService;
        _milestoneService = milestoneService;
        _reviewService = reviewService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllProposals()
    {
        try
        {
            var proposal = await _proposalService.GetAllProposalsAsync();
            return Ok(proposal);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetProposalById(Guid id)
    {
        try
        {
            var proposal = await _proposalService.GetProposalByIdAsync(id);
            return Ok(proposal);
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

    [HttpGet("{id}/review")]
    public async Task<IActionResult> GetReviewByProposalId(Guid id)
    {
        try
        {
            var proposal = await _reviewService.GetReviewByProposalIdAsync(id);
            return Ok(proposal);
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

    [HttpGet("/api/chats/{chatId}/[controller]")]
    [Authorize]
    public async Task<IActionResult> GetProposalByChatId(Guid chatId)
    {
        try
        {
            var proposal = await _proposalService.GetProposalsByChatIdAsync(chatId);
            return Ok(proposal);
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

    [HttpGet("/api/creators/{accountId}/[controller]")]
    [Authorize]
    public async Task<IActionResult> GetProposalByCreatorId(Guid accountId)
    {
        try
        {
            var proposal = await _proposalService.GetProposalsByCreatorIdIdAsync(accountId);
            return Ok(proposal);
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

    [HttpGet("/api/audiences/{audienceId}/[controller]")]
    [Authorize]
    public async Task<IActionResult> GetProposalByAudienceId(Guid audienceId)
    {
        try
        {
            var proposal = await _proposalService.GetProposalsByAudienceIdIdAsync(audienceId);
            return Ok(proposal);
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

    [HttpGet("/api/services/{serviceId}/[controller]")]
    [Authorize]
    public async Task<IActionResult> GetProposalByServiceId(Guid serviceId)
    {
        try
        {
            var proposal = await _proposalService.GetProposalsByServiceIdAsync(serviceId);
            return Ok(proposal);
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> PostProposal(ProposalModel model)
    {
        try
        {
            var proposal = await _proposalService.CreateProposalAsync(model);
            return CreatedAtAction("GetProposalById", new { id = proposal.Id }, proposal);
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

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutProposal(Guid id, ProposalUpdateStatusModel model)
    {
        try
        {
            var proposal = await _proposalService.UpdateProposalStatusAsync(id, model);
            return Ok(proposal);
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

    [HttpGet("milestones/{proposalId}")]
    [Authorize]
    public async Task<IActionResult> GetAllMilestinesOfProposal(Guid proposalId)
    {
        try
        {
            var milestones = await _milestoneService.GetMilestonesAsync(proposalId);
            return Ok(milestones);
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

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProposal(Guid id)
    {
        try
        {
            await _proposalService.DeleteProposalAsync(id);
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

    [HttpPost("init-payment/{id}")]
    [Authorize]
    public async Task<IActionResult> InitPaymentProposal(Guid id)
    {
        Console.WriteLine($"[InitPayment] Proposal ID: {id}");
        try
        {
            var transaction = await _proposalService.InitPaymentProposalAsync(id);
            Console.WriteLine($"[InitPayment] Success! Transaction: {transaction}");
            return Ok(transaction);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"[InitPayment] NotFound: {ex.Message}");
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (BadHttpRequestException ex)
        {
            Console.WriteLine($"[InitPayment] BadRequest: {ex.Message}");
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"[InitPayment] ArgumentOutOfRange: {ex.Message}");
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"[InitPayment] Unauthorized: {ex.Message}");
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[InitPayment] ERROR: {ex.Message}");
            Console.WriteLine($"[InitPayment] StackTrace: {ex.StackTrace}");
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpPost("complete-payment/{id}")]
    [Authorize]
    public async Task<IActionResult> CompletePaymentProposal(Guid id)
    {
        try
        {
            var transaction = await _proposalService.CompletePaymentProposalAsync(id);
            return Ok(transaction);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpPost("{id}/confirm")]
    public  async Task<IActionResult> ConfirmPayment(Guid id)
    {
        try
        {
            await _proposalService.ConfirmPaymentProposalAsync(id);
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
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }
}
