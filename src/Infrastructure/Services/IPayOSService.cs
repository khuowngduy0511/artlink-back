namespace Infrastructure.Services;

public interface IPayOSService
{
    /// <summary>
    /// Tạo link thanh toán nạp tiền vào ví
    /// </summary>
    Task<string> CreateDepositPaymentLinkAsync(decimal amount, string description);

    /// <summary>
    /// Tạo link thanh toán mua asset
    /// </summary>
    Task<string> CreateAssetPaymentLinkAsync(Guid assetId, decimal price, string assetTitle);

    /// <summary>
    /// Verify webhook signature từ PayOS
    /// </summary>
    Task<object?> VerifyWebhookAsync(object webhookData);

    /// <summary>
    /// Xử lý callback từ PayOS sau khi thanh toán
    /// </summary>
    Task<bool> HandlePaymentCallbackAsync(object webhookData);

    /// <summary>
    /// Xử lý returnUrl - cập nhật ví sau khi thanh toán thành công
    /// </summary>
    Task<bool> ProcessReturnUrlAsync(Guid transactionId);

    /// <summary>
    /// Hủy thanh toán
    /// </summary>
    Task<object> CancelPaymentAsync(long orderCode, string? reason = null);

    /// <summary>
    /// Lấy trạng thái thanh toán
    /// </summary>
    Task<object> GetPaymentStatusAsync(long orderCode);
}
