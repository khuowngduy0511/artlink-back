using Application.Commons;
using Application.Models;
using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly IWalletHistoryService _walletHistoryService;
    private readonly IWalletService _walletService;
    private readonly IClaimService _claimService;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentsController(
        IWalletHistoryService walletHistoryService,
        IWalletService walletService,
        IClaimService claimService,
        IUnitOfWork unitOfWork)
    {
        _walletHistoryService = walletHistoryService;
        _walletService = walletService;
        _claimService = claimService;
        _unitOfWork = unitOfWork;
    }

    [HttpPut("update-bank-account")]
    [Authorize]
    public async Task<IActionResult> UpdateBankAccount([FromBody] UpdateBankAccountModel model)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(model.BankAccountNumber))
            {
                return BadRequest("Số tài khoản không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(model.BankCode))
            {
                return BadRequest("Vui lòng chọn ngân hàng.");
            }

            // Cập nhật thông tin ngân hàng vào ví
            await _walletService.UpdateCurrentWalletAsync(new WalletEM 
            { 
                WithdrawInformation = model.BankAccountNumber,
                BankCode = model.BankCode,
                BankName = model.BankName
            });

            return Ok(new { Message = "Cập nhật thông tin ngân hàng thành công." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("bank-list")]
    public IActionResult GetBankList()
    {
        try
        {
            var banks = new[]
            {
                new { code = "970415", name = "Ngân hàng TMCP Công Thương Việt Nam", shortName = "VietinBank" },
                new { code = "970436", name = "Ngân hàng TMCP Ngoại Thương Việt Nam", shortName = "Vietcombank" },
                new { code = "970422", name = "Ngân hàng TMCP Quân Đội", shortName = "MB Bank" },
                new { code = "970418", name = "Ngân hàng TMCP Đầu tư và Phát triển Việt Nam", shortName = "BIDV" },
                new { code = "970405", name = "Ngân hàng Nông nghiệp và Phát triển nông thôn", shortName = "Agribank" },
                new { code = "970407", name = "Ngân hàng TMCP Kỹ Thương Việt Nam", shortName = "Techcombank" },
                new { code = "970432", name = "Ngân hàng TMCP Việt Nam Thịnh Vượng", shortName = "VPBank" },
                new { code = "970423", name = "Ngân hàng TMCP Tiên Phong", shortName = "TPBank" },
                new { code = "970403", name = "Ngân hàng TMCP Sài Gòn Thương Tín", shortName = "Sacombank" },
                new { code = "970437", name = "Ngân hàng TMCP Phát triển Thành phố Hồ Chí Minh", shortName = "HDBank" },
                new { code = "970416", name = "Ngân hàng TMCP Á Châu", shortName = "ACB" },
                new { code = "970414", name = "Ngân hàng TMCP Đại Chúng Việt Nam", shortName = "PVcomBank" },
                new { code = "970448", name = "Ngân hàng TMCP Phương Đông", shortName = "OCB" },
                new { code = "970438", name = "Ngân hàng TMCP Bản Việt", shortName = "VietCapital Bank" },
                new { code = "970441", name = "Ngân hàng TMCP Quốc tế Việt Nam", shortName = "VIB" },
                new { code = "970443", name = "Ngân hàng TMCP Sài Gòn - Hà Nội", shortName = "SHB" },
                new { code = "970431", name = "Ngân hàng TMCP Xuất Nhập khẩu Việt Nam", shortName = "Eximbank" },
                new { code = "970426", name = "Ngân hàng TMCP Hàng Hải Việt Nam", shortName = "MSB" },
                new { code = "970406", name = "Ngân hàng TMCP Đông Á", shortName = "DongA Bank" },
                new { code = "970412", name = "Ngân hàng TMCP Bưu điện Liên Việt", shortName = "LienVietPostBank" },
                new { code = "970424", name = "Ngân hàng TMCP Đại Dương", shortName = "OceanBank" },
                new { code = "970430", name = "Ngân hàng TMCP Bắc Á", shortName = "BacA Bank" },
                new { code = "970433", name = "Ngân hàng TMCP Việt Á", shortName = "VietA Bank" },
                new { code = "970427", name = "Ngân hàng TMCP Việt Nam Thương Tín", shortName = "VietBank" },
                new { code = "970440", name = "Ngân hàng TMCP Sài Gòn Công Thương", shortName = "Saigonbank" },
                new { code = "970454", name = "Ngân hàng TMCP Bảo Việt", shortName = "BaoViet Bank" },
                new { code = "970429", name = "Ngân hàng TMCP Sài Gòn", shortName = "SCB" },
                new { code = "970419", name = "Ngân hàng TMCP Quốc Dân", shortName = "NCB" },
                new { code = "970458", name = "Ngân hàng TMCP Đại Tín", shortName = "UOB" },
                new { code = "970410", name = "Ngân hàng TMCP Nam Á", shortName = "Nam A Bank" }
            };

            return Ok(banks);
        }
        catch (Exception ex)
        {
            Log.Error($"Error in GetBankList: {ex.Message}", ex);
            return StatusCode(500, new { Message = "Đã xảy ra lỗi khi lấy danh sách ngân hàng.", Error = ex.Message });
        }
    }

    /// <summary>
    /// User tạo yêu cầu rút tiền
    /// </summary>
    [HttpPost("withdrawal-request")]
    [Authorize]
    public async Task<IActionResult> CreateWithdrawalRequest([FromBody] CreateWithdrawalRequestEM model)
    {
        try
        {
            var currentUserId = _claimService.GetCurrentUserId ?? Guid.Empty;
            
            // Lấy thông tin ví
            var wallet = await _walletService.GetWalletByAccountIdAsync(currentUserId);
            if (wallet == null)
            {
                return BadRequest(new { Message = "Người dùng này chưa kích hoạt ví." });
            }

            // Kiểm tra thông tin ngân hàng
            if (string.IsNullOrEmpty(wallet.BankCode) || string.IsNullOrEmpty(wallet.BankName) || 
                string.IsNullOrEmpty(wallet.WithdrawInformation))
            {
                return BadRequest(new { Message = "Vui lòng cập nhật thông tin ngân hàng trước khi rút tiền." });
            }

            // Kiểm tra số dư
            if (wallet.Balance < model.Amount)
            {
                return BadRequest(new { Message = "Số dư không đủ để thực hiện giao dịch." });
            }

            if (model.Amount < 10000)
            {
                return BadRequest(new { Message = "Số tiền rút tối thiểu là 10,000 Xu." });
            }

            // Tạo yêu cầu rút tiền
            var withdrawalRequest = new WithdrawalRequest
            {
                WalletId = wallet.Id,
                Amount = model.Amount,
                BankCode = wallet.BankCode,
                BankName = wallet.BankName,
                BankAccountNumber = wallet.WithdrawInformation,
                Status = "Pending",
                WalletBalanceSnapshot = wallet.Balance,
                CreatedOn = DateTime.UtcNow
            };

            await _unitOfWork.WithdrawalRequestRepository.AddAsync(withdrawalRequest);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { 
                Message = "Yêu cầu rút tiền đã được gửi thành công. Chúng tôi sẽ xử lý trong 1-3 ngày làm việc.",
                RequestId = withdrawalRequest.Id
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Đã xảy ra lỗi khi tạo yêu cầu rút tiền.", Error = ex.Message });
        }
    }

    /// <summary>
    /// User xem danh sách yêu cầu rút tiền của mình
    /// </summary>
    [HttpGet("withdrawal-requests")]
    [Authorize]
    public async Task<IActionResult> GetMyWithdrawalRequests()
    {
        try
        {
            var currentUserId = _claimService.GetCurrentUserId ?? Guid.Empty;
            var wallet = await _walletService.GetWalletByAccountIdAsync(currentUserId);
            
            if (wallet == null)
            {
                return Ok(new List<WithdrawalRequestVM>());
            }

            var requests = await _unitOfWork.WithdrawalRequestRepository
                .GetListByConditionAsync(x => x.WalletId == wallet.Id);

            var result = requests.OrderByDescending(x => x.CreatedOn)
                .Select(x => new WithdrawalRequestVM
                {
                    Id = x.Id,
                    WalletId = x.WalletId,
                    Amount = x.Amount,
                    BankCode = x.BankCode,
                    BankName = x.BankName,
                    BankAccountNumber = x.BankAccountNumber,
                    BankAccountName = x.BankAccountName,
                    Status = x.Status,
                    AdminNote = x.AdminNote,
                    ProcessedBy = x.ProcessedBy,
                    ProcessedOn = x.ProcessedOn,
                    WalletBalanceSnapshot = x.WalletBalanceSnapshot,
                    CreatedOn = x.CreatedOn
                }).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Đã xảy ra lỗi khi lấy danh sách yêu cầu rút tiền.", Error = ex.Message });
        }
    }

    /// <summary>
    /// Admin xem tất cả yêu cầu rút tiền
    /// </summary>
    [HttpGet("admin/withdrawal-requests")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> GetAllWithdrawalRequests([FromQuery] string? status = null)
    {
        try
        {
            var allRequests = await _unitOfWork.WithdrawalRequestRepository.GetAllAsync();

            List<WithdrawalRequest> requests;
            if (!string.IsNullOrEmpty(status))
            {
                requests = allRequests.Where(x => x.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                requests = allRequests;
            }

            var result = requests.OrderByDescending(x => x.CreatedOn)
                .Select(x => new WithdrawalRequestVM
                {
                    Id = x.Id,
                    WalletId = x.WalletId,
                    Amount = x.Amount,
                    BankCode = x.BankCode,
                    BankName = x.BankName,
                    BankAccountNumber = x.BankAccountNumber,
                    BankAccountName = x.BankAccountName,
                    Status = x.Status,
                    AdminNote = x.AdminNote,
                    ProcessedBy = x.ProcessedBy,
                    ProcessedOn = x.ProcessedOn,
                    WalletBalanceSnapshot = x.WalletBalanceSnapshot,
                    CreatedOn = x.CreatedOn
                }).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Đã xảy ra lỗi khi lấy danh sách yêu cầu rút tiền.", Error = ex.Message });
        }
    }

    /// <summary>
    /// Admin xử lý yêu cầu rút tiền (Approve/Reject/Complete)
    /// </summary>
    [HttpPut("admin/withdrawal-requests/{id}")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> ProcessWithdrawalRequest(Guid id, [FromBody] ProcessWithdrawalRequestEM model)
    {
        try
        {
            var currentUserId = _claimService.GetCurrentUserId ?? Guid.Empty;
            var request = await _unitOfWork.WithdrawalRequestRepository.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound(new { Message = "Không tìm thấy yêu cầu rút tiền." });
            }

            if (request.Status != "Pending" && model.Status == "Approved")
            {
                return BadRequest(new { Message = "Chỉ có thể duyệt yêu cầu đang ở trạng thái Pending." });
            }

            // Nếu admin từ chối hoặc hoàn thành
            if (model.Status == "Rejected" || model.Status == "Completed")
            {
                request.Status = model.Status;
                request.AdminNote = model.AdminNote;
                request.ProcessedBy = currentUserId;
                request.ProcessedOn = DateTime.UtcNow;

                // Nếu hoàn thành, trừ tiền trong ví
                if (model.Status == "Completed")
                {
                    var wallet = await _unitOfWork.WalletRepository.GetByIdAsync(request.WalletId);
                    if (wallet != null)
                    {
                        wallet.Balance -= request.Amount;
                        _unitOfWork.WalletRepository.Update(wallet);

                        // Tạo wallet history
                        var walletHistory = new WalletHistory
                        {
                            Amount = -request.Amount,
                            Type = WalletHistoryTypeEnum.Withdraw,
                            WalletBalance = wallet.Balance,
                            TransactionStatus = TransactionStatusEnum.Success,
                            PaymentMethod = PaymentMethodEnum.BankTransfer,
                            CreatedBy = wallet.AccountId,
                            CreatedOn = DateTime.UtcNow
                        };
                        await _unitOfWork.WalletHistoryRepository.AddAsync(walletHistory);
                    }
                }

                _unitOfWork.WithdrawalRequestRepository.Update(request);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new { Message = $"Đã {(model.Status == "Completed" ? "hoàn thành" : "từ chối")} yêu cầu rút tiền." });
            }

            if (model.Status == "Approved")
            {
                request.Status = "Approved";
                request.AdminNote = model.AdminNote;
                request.ProcessedBy = currentUserId;
                request.ProcessedOn = DateTime.UtcNow;

                _unitOfWork.WithdrawalRequestRepository.Update(request);
                await _unitOfWork.SaveChangesAsync();

                return Ok(new { Message = "Đã duyệt yêu cầu rút tiền." });
            }

            return BadRequest(new { Message = "Trạng thái không hợp lệ." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Đã xảy ra lỗi khi xử lý yêu cầu rút tiền.", Error = ex.Message });
        }
    }
}

public class UpdateBankAccountModel
{
    public string BankAccountNumber { get; set; } = default!;
    public string BankCode { get; set; } = default!;
    public string BankName { get; set; } = default!;
}

// for testing the callback api
public class EncodeBodyModel
{
    public string Key { get; set; } = default!;
    public string EncodeData { get; set; } = default!;
}
