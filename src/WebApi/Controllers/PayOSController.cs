using Application.Services.Abstractions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/payos")]
[ApiController]
public class PayOSController : ControllerBase
{
    private readonly IPayOSService _payOSService;
    private readonly IClaimService _claimService;
    private readonly ILogger<PayOSController> _logger;

    public PayOSController(
        IPayOSService payOSService,
        IClaimService claimService,
        ILogger<PayOSController> logger)
    {
        _payOSService = payOSService;
        _claimService = claimService;
        _logger = logger;
    }

    /// <summary>
    /// Tạo link thanh toán nạp tiền vào ví
    /// </summary>
    [HttpPost("deposit")]
    [Authorize]
    public async Task<IActionResult> CreateDepositPaymentLink([FromBody] DepositRequest request)
    {
        try
        {
            if (request.Amount <= 0)
            {
                return BadRequest(new ApiResponse 
                { 
                    ErrorMessage = "Số tiền phải lớn hơn 0" 
                });
            }

            if (request.Amount < 1000)
            {
                return BadRequest(new ApiResponse 
                { 
                    ErrorMessage = "Số tiền nạp tối thiểu là 1,000 VNĐ" 
                });
            }

            // Description will be generated inside the service (max 25 chars for PayOS)
            var paymentUrl = await _payOSService.CreateDepositPaymentLinkAsync(
                request.Amount,
                $"Nạp tiền vào ví"  // This is stored in DB, not sent to PayOS
            );

            return Ok(new 
            { 
                PaymentUrl = paymentUrl,
                Message = "Tạo link thanh toán thành công" 
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating deposit payment link");
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    /// <summary>
    /// Tạo link thanh toán mở khóa asset (mua tác phẩm)
    /// </summary>
    [HttpPost("asset/{assetId}")]
    [Authorize]
    public async Task<IActionResult> CreateAssetPaymentLink(Guid assetId)
    {
        try
        {
            // TODO: Get asset info to show title
            var paymentUrl = await _payOSService.CreateAssetPaymentLinkAsync(
                assetId,
                100000, // Price will be fetched from database
                "Artwork Asset" // Title will be fetched from database
            );

            return Ok(new 
            { 
                PaymentUrl = paymentUrl,
                Message = "Tạo link thanh toán thành công" 
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating asset payment link");
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    /// <summary>
    /// Webhook callback từ PayOS - xử lý thanh toán thành công/thất bại
    /// </summary>
    [HttpPost("webhook")]
    public async Task<IActionResult> PayOSWebhook()
    {
        try
        {
            // Đọc body
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            
            _logger.LogInformation("PayOS Webhook received: {Body}", body);

            // Deserialize webhook data
            var webhookData = JsonSerializer.Deserialize<object>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (webhookData == null)
            {
                return BadRequest(new { message = "Invalid webhook data" });
            }

            // Xử lý callback (verification được thực hiện bên trong)
            var success = await _payOSService.HandlePaymentCallbackAsync(webhookData);

            if (success)
            {
                _logger.LogInformation("Payment processed successfully");
                
                return Ok(new 
                { 
                    error = 0,
                    message = "Payment processed successfully"
                });
            }

            _logger.LogError("Failed to process payment");
            
            return BadRequest(new 
            { 
                error = -1,
                message = "Payment processing failed" 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing PayOS webhook");
            return StatusCode(500, new 
            { 
                error = -1,
                message = ex.Message 
            });
        }
    }

    /// <summary>
    /// Xử lý callback khi thanh toán thành công - được gọi từ returnUrl
    /// </summary>
    [HttpGet("return")]
    public async Task<IActionResult> PaymentReturn([FromQuery] string txId, [FromQuery] string? status)
    {
        try
        {
            if (string.IsNullOrEmpty(txId))
            {
                return BadRequest("Missing transaction ID");
            }

            _logger.LogInformation("[PayOS Return] TxId: {TxId}, Status: {Status}", txId, status);

            // Parse transaction ID
            if (!Guid.TryParse(txId, out var transactionId))
            {
                return BadRequest("Invalid transaction ID");
            }

            // Xử lý cập nhật ví
            var success = await _payOSService.ProcessReturnUrlAsync(transactionId);

            if (success)
            {
                // Redirect về frontend với thông báo thành công
                return Redirect($"https://artlink-front.vercel.app/payment/success?txId={txId}");
            }

            return Redirect($"https://artlink-front.vercel.app/payment/cancel?txId={txId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing payment return");
            return Redirect($"https://artlink-front.vercel.app/payment/cancel?error={ex.Message}");
        }
    }

    /// <summary>
    /// Xử lý khi user hủy thanh toán - được gọi từ cancelUrl
    /// </summary>
    [HttpGet("cancel")]
    public IActionResult PaymentCancel([FromQuery] string txId)
    {
        _logger.LogInformation("[PayOS Cancel] TxId: {TxId}", txId);
        return Redirect($"https://artlink-front.vercel.app/payment/cancel?txId={txId}");
    }

    /// <summary>
    /// Kiểm tra trạng thái thanh toán
    /// </summary>
    [HttpGet("status/{orderCode}")]
    [Authorize]
    public async Task<IActionResult> GetPaymentStatus(long orderCode)
    {
        try
        {
            var status = await _payOSService.GetPaymentStatusAsync(orderCode);
            return Ok(status);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting payment status for order {OrderCode}", orderCode);
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    /// <summary>
    /// Hủy thanh toán
    /// </summary>
    [HttpPost("cancel/{orderCode}")]
    [Authorize]
    public async Task<IActionResult> CancelPayment(long orderCode, [FromBody] CancelPaymentRequest request)
    {
        try
        {
            var result = await _payOSService.CancelPaymentAsync(orderCode, request.Reason ?? "User cancelled");
            
            return Ok(new 
            { 
                Message = "Hủy thanh toán thành công",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling payment for order {OrderCode}", orderCode);
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
    }
}

public class DepositRequest
{
    public decimal Amount { get; set; }
}

public class CancelPaymentRequest
{
    public string? Reason { get; set; }
}
