using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SendMailExampleController : ControllerBase
{
    private readonly IEmailService _emailService;

    public SendMailExampleController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    // POST: api/SendMailExample
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SendMailExample()
    {
        try
        {
            await _emailService.CreateSampleMailAsync();
            return Ok(new { Message = "Đã gửi mail thành công" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }
}