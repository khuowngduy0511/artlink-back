using Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SoftwareUsedsController : ControllerBase
{
    private readonly ISoftwareUsedService _softwareUsedService;

    public SoftwareUsedsController(ISoftwareUsedService softwareUsedService)
    {
        _softwareUsedService = softwareUsedService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSoftwareUseds()
    {
        try
        {
            var softwareUseds = await _softwareUsedService.GetAllSoftwareUsedAsync();
            return Ok(softwareUseds);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("{softwareUsedId}")]
    public async Task<IActionResult> GetSoftwareUsedById(Guid softwareUsedId)
    {
        try
        {
            var softwareUsed = await _softwareUsedService.GetSoftwareUsedByIdAsync(softwareUsedId);
            return Ok(softwareUsed);
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
