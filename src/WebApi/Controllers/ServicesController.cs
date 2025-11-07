using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;
namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;
    private readonly IReviewService _reviewService;

    public ServicesController(
        IServiceService serviceService
        , IReviewService reviewService)
    {
        _serviceService = serviceService;
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllServices([FromQuery] ServiceCriteria criteria)
    {
        try
        {
            var listService = await _serviceService.GetAllServicesAsync(criteria);
            return Ok(listService);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("{serviceId}")]
    public async Task<IActionResult> GetServiceById(Guid serviceId)
    {
        try
        {
            var serviceVM = await _serviceService.GetServiceByIdAsync(serviceId);
            return Ok(serviceVM);
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

    [HttpGet("{serviceId}/reviews")]
    public async Task<IActionResult> GetReviewsByServiceId(Guid serviceId, [FromQuery] PagedCriteria criteria)
    {
        try
        {
            var serviceVM = await _reviewService.GetReviewsByServiceIdAsync(serviceId, criteria);
            return Ok(serviceVM);
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
    public async Task<IActionResult> AddService([FromForm] ServiceModel serviceModel)
    {
        try
        {
            var serviceVM = await _serviceService.AddServiceAsync(serviceModel);
            return CreatedAtAction(nameof(GetServiceById),
                new { serviceId = serviceVM.Id }, serviceVM);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"[AddService Error] KeyNotFoundException: {ex.Message}");
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (BadHttpRequestException ex)
        {
            Console.WriteLine($"[AddService Error] BadHttpRequestException: {ex.Message}");
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"[AddService Error] UnauthorizedAccessException: {ex.Message}");
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[AddService Error] Exception: {ex.Message}");
            Console.WriteLine($"[AddService Error] StackTrace: {ex.StackTrace}");
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpDelete("{serviceId}")]
    [Authorize]
    public async Task<IActionResult> DeleteService(Guid serviceId)
    {
        try
        {
            await _serviceService.DeleteServiceAsync(serviceId);
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

    [HttpPut("{serviceId}")]
    [Authorize]
    public async Task<IActionResult> UpdateService(Guid serviceId, [FromForm] ServiceEM serviceEM)
    {
        try
        {
            await _serviceService.UpdateServiceAsync(serviceId, serviceEM);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (BadHttpRequestException ex)
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
