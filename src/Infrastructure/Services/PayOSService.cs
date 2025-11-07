using Application.AppConfigurations;
using Application.Commons;
using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Enums;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
namespace Infrastructure.Services;

public class PayOSService : IPayOSService
{
    private readonly PayOSConfiguration _config;
    private readonly IClaimService _claimService;
    private readonly AppDBContext _dbContext;
    private readonly HttpClient _httpClient;
    private readonly ILogger<PayOSService> _logger;

    public PayOSService(
        AppConfiguration appConfiguration,
        IClaimService claimService,
        AppDBContext dbContext,
        IHttpClientFactory httpClientFactory,
        ILogger<PayOSService> logger)
    {
        _config = appConfiguration.PayOSConfiguration;
        _claimService = claimService;
        _dbContext = dbContext;
        _logger = logger;
        
        // PayOS v2 - Only needs x-client-id and x-api-key headers, NO signature!
        _httpClient = httpClientFactory.CreateClient("PayOS");
        _httpClient.BaseAddress = new Uri("https://api-merchant.payos.vn");
        _httpClient.DefaultRequestHeaders.Add("x-client-id", _config.ClientId);
        _httpClient.DefaultRequestHeaders.Add("x-api-key", _config.ApiKey);
    }

    public async Task<string> CreateDepositPaymentLinkAsync(decimal amount, string description)
    {
        var userId = _claimService.GetCurrentUserId;
        if (!userId.HasValue || userId.Value == Guid.Empty)
        {
            throw new UnauthorizedAccessException("User not authenticated");
        }

        var orderCode = GenerateOrderCode();
        
        // Tạo transaction history với status pending
        var transaction = new TransactionHistory
        {
            Id = Guid.NewGuid(),
            CreatedBy = userId.Value,
            CreatedOn = DateTime.UtcNow,
            Detail = description,
            Price = (double)amount,
            Fee = 0,
            TransactionStatus = Domain.Enums.TransactionStatusEnum.InProgress,
            PaymentMethod = "PayOS",
            PaymentOrderCode = orderCode
        };
        
        _dbContext.TransactionHistories.Add(transaction);
        await _dbContext.SaveChangesAsync();

        // PayOS v2: Build URLs and description
        var cancelUrl = $"{_config.CancelUrl}?txId={transaction.Id}";
        var returnUrl = $"{_config.ReturnUrl}?txId={transaction.Id}";
        var desc = $"Deposit-{orderCode}";  // Keep it short (9 chars limit for non-linked banks)
        var amt = (int)amount;

        // Generate signature with the EXACT URLs (including query parameters)
        var signature = GenerateSignature(amt, cancelUrl, desc, orderCode, returnUrl);

        // Build payload with signature in body
        var paymentData = new
        {
            orderCode = orderCode,
            amount = amt,
            description = desc,
            items = new[]
            {
                new
                {
                    name = "Deposit",
                    quantity = 1,
                    price = amt
                }
            },
            cancelUrl = cancelUrl,
            returnUrl = returnUrl,
            signature = signature  // Signature goes in BODY, not header
        };

        var request = new HttpRequestMessage(HttpMethod.Post, "/v2/payment-requests");
        // No x-signature header needed - signature is in body
        request.Content = new StringContent(
            JsonSerializer.Serialize(paymentData),
            Encoding.UTF8,
            "application/json"
        );

        _logger.LogInformation("[PayOS v2] OrderCode: {OrderCode}, Amount: {Amount}", orderCode, amount);
        _logger.LogInformation("[PayOS v2] Request Body: {Body}", JsonSerializer.Serialize(paymentData));
        _logger.LogInformation("[PayOS v2] Signature: {Signature}", signature);

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("[PayOS v2] HTTP Error - Status: {StatusCode}, Response: {Response}", response.StatusCode, responseContent);
            throw new Exception($"PayOS API Error [{response.StatusCode}]: {responseContent}");
        }

        var result = JsonSerializer.Deserialize<PayOSPaymentResponse>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (result == null)
        {
            _logger.LogError("[PayOS v2] Invalid response format");
            throw new Exception("Failed to parse PayOS response");
        }

        if (result.code != "00")
        {
            _logger.LogError("[PayOS v2] API Error - Code: {Code}, Desc: {Desc}", result.code, result.desc);
            throw new Exception($"PayOS returned error code {result.code}: {result.desc}");
        }

        _logger.LogInformation("[PayOS v2] Success - CheckoutUrl: {CheckoutUrl}", result.data?.checkoutUrl);
        return result.data?.checkoutUrl ?? throw new Exception("CheckoutUrl is null");
    }

    public async Task<string> CreateAssetPaymentLinkAsync(Guid assetId, decimal price, string assetTitle)
    {
        var userId = _claimService.GetCurrentUserId;
        if (!userId.HasValue || userId.Value == Guid.Empty)
        {
            throw new UnauthorizedAccessException("User not authenticated");
        }

        // Kiểm tra asset tồn tại
        var asset = await _dbContext.Assets
            .Include(a => a.Artwork)
            .ThenInclude(aw => aw.Account)
            .FirstOrDefaultAsync(a => a.Id == assetId);

        if (asset == null)
        {
            throw new KeyNotFoundException($"Asset {assetId} not found");
        }

        // Kiểm tra user đã mua chưa
        var existingTransaction = await _dbContext.TransactionHistories
            .AnyAsync(t => t.AssetId == assetId && t.CreatedBy == userId.Value && t.TransactionStatus == Domain.Enums.TransactionStatusEnum.Success);

        if (existingTransaction)
        {
            throw new InvalidOperationException("You already own this asset");
        }

        var orderCode = GenerateOrderCode();
        var sellerId = asset.Artwork.CreatedBy;
        
        // Tính phí platform (10%)
        var platformFee = price * 0.10m;
        var sellerRevenue = price - platformFee;

        // Tạo transaction history
        var transaction = new TransactionHistory
        {
            Id = Guid.NewGuid(),
            AssetId = assetId,
            CreatedBy = userId.Value, // Buyer
            ToAccountId = sellerId, // Seller
            CreatedOn = DateTime.UtcNow,
            Detail = $"Mở khóa tài nguyên \"{assetTitle}\"",
            Price = -(double)price, // Buyer trả tiền
            Fee = (double)platformFee,
            TransactionStatus = Domain.Enums.TransactionStatusEnum.InProgress,
            PaymentMethod = "PayOS",
            PaymentOrderCode = orderCode
        };
        
        _dbContext.TransactionHistories.Add(transaction);
        await _dbContext.SaveChangesAsync();

        // Build URLs and description
        var cancelUrl = $"{_config.CancelUrl}?txId={transaction.Id}";
        var returnUrl = $"{_config.ReturnUrl}?txId={transaction.Id}&assetId={assetId}";
        var desc = $"Asset-{orderCode}";  // Keep it short
        var amt = (int)price;

        // Generate signature with exact URLs
        var signature = GenerateSignature(amt, cancelUrl, desc, orderCode, returnUrl);

        // Build payload with signature in body
        var paymentData = new
        {
            orderCode = orderCode,
            amount = amt,
            description = desc,
            items = new[]
            {
                new
                {
                    name = assetTitle.Length > 20 ? assetTitle.Substring(0, 20) : assetTitle,
                    quantity = 1,
                    price = amt
                }
            },
            cancelUrl = cancelUrl,
            returnUrl = returnUrl,
            signature = signature  // Signature goes in BODY, not header
        };
        
        var request = new HttpRequestMessage(HttpMethod.Post, "/v2/payment-requests");
        // No x-signature header - signature is in body
        request.Content = new StringContent(
            JsonSerializer.Serialize(paymentData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"PayOS API Error: {responseContent}");
        }

        var result = JsonSerializer.Deserialize<PayOSPaymentResponse>(responseContent);
        return result?.data?.checkoutUrl ?? throw new Exception("Failed to create payment link");
    }

    public async Task<object?> VerifyWebhookAsync(object webhookData)
    {
        try
        {
            // Simple verification - in production, verify signature
            return await Task.FromResult(webhookData);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> HandlePaymentCallbackAsync(object webhookData)
    {
        try
        {
            // Verify webhook
            var verifiedWebhook = await VerifyWebhookAsync(webhookData);
            if (verifiedWebhook == null)
            {
                return false;
            }

            var json = JsonSerializer.Serialize(webhookData);
            var webhook = JsonSerializer.Deserialize<PayOSWebhook>(json);

            if (webhook?.code != "00" || webhook?.success != true)
            {
                // Payment failed
                return false;
            }

            if (webhook.data == null)
            {
                return false;
            }

            // Extract transaction ID from description
            var description = webhook.data.description;
            var txIdMatch = System.Text.RegularExpressions.Regex.Match(description, @"TxId: ([a-f0-9\-]+)");
            
            if (!txIdMatch.Success)
            {
                return false;
            }

            var transactionId = Guid.Parse(txIdMatch.Groups[1].Value);
            var transaction = await _dbContext.TransactionHistories
                .Include(t => t.Asset)
                .FirstOrDefaultAsync(t => t.Id == transactionId);

            if (transaction == null || transaction.TransactionStatus != Domain.Enums.TransactionStatusEnum.InProgress)
            {
                return false;
            }

            // Update transaction status
            transaction.TransactionStatus = Domain.Enums.TransactionStatusEnum.Success;
            transaction.PaymentTransactionId = webhook.data.orderCode.ToString();

            if (transaction.AssetId.HasValue)
            {
                // Asset purchase - cập nhật wallet cho seller
                var sellerWallet = await _dbContext.Wallets
                    .FirstOrDefaultAsync(w => w.AccountId == transaction.ToAccountId);

                if (sellerWallet != null)
                {
                    var sellerRevenue = -transaction.Price - transaction.Fee; // Price is negative for buyer
                    sellerWallet.Balance += sellerRevenue;
                    
                    // Tạo transaction cho seller
                    var sellerTransaction = new TransactionHistory
                    {
                        Id = Guid.NewGuid(),
                        AssetId = transaction.AssetId,
                        CreatedBy = transaction.ToAccountId.Value,
                        ToAccountId = transaction.CreatedBy,
                        CreatedOn = DateTime.UtcNow,
                        Detail = transaction.Detail,
                        Price = sellerRevenue,
                        Fee = transaction.Fee,
                        TransactionStatus = Domain.Enums.TransactionStatusEnum.Success,
                        WalletBalance = sellerWallet.Balance,
                        PaymentMethod = "PayOS",
                        PaymentOrderCode = transaction.PaymentOrderCode,
                        PaymentTransactionId = transaction.PaymentTransactionId
                    };
                    
                    _dbContext.TransactionHistories.Add(sellerTransaction);
                }
            }
            else
            {
                // Deposit - cập nhật wallet cho buyer
                var buyerWallet = await _dbContext.Wallets
                    .FirstOrDefaultAsync(w => w.AccountId == transaction.CreatedBy);

                if (buyerWallet != null)
                {
                    // Price trong deposit là DƯƠNG (positive), cộng trực tiếp vào ví
                    buyerWallet.Balance += transaction.Price;
                    transaction.WalletBalance = buyerWallet.Balance;
                }
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ProcessReturnUrlAsync(Guid transactionId)
    {
        try
        {
            var transaction = await _dbContext.TransactionHistories
                .Include(t => t.Asset)
                .FirstOrDefaultAsync(t => t.Id == transactionId);

            if (transaction == null || transaction.TransactionStatus != Domain.Enums.TransactionStatusEnum.InProgress)
            {
                _logger.LogWarning("[PayOS Return] Transaction not found or not in progress: {TxId}", transactionId);
                return false;
            }

            // Lấy thông tin thanh toán từ PayOS API để verify
            var orderCode = transaction.PaymentOrderCode ?? 0;
            var paymentStatus = await GetPaymentStatusAsync(orderCode);
            
            _logger.LogInformation("[PayOS Return] Payment status for order {OrderCode}: {Status}", orderCode, JsonSerializer.Serialize(paymentStatus));

            // Cập nhật transaction status
            transaction.TransactionStatus = Domain.Enums.TransactionStatusEnum.Success;

            if (transaction.AssetId.HasValue)
            {
                // Asset purchase - cập nhật wallet cho seller
                var sellerWallet = await _dbContext.Wallets
                    .FirstOrDefaultAsync(w => w.AccountId == transaction.ToAccountId);

                if (sellerWallet != null)
                {
                    var sellerRevenue = -transaction.Price - transaction.Fee; // Price is negative for buyer
                    sellerWallet.Balance += sellerRevenue;
                    
                    _logger.LogInformation("[PayOS Return] Updated seller wallet. Balance: {Balance}", sellerWallet.Balance);
                    
                    // Tạo transaction cho seller
                    var sellerTransaction = new TransactionHistory
                    {
                        Id = Guid.NewGuid(),
                        AssetId = transaction.AssetId,
                        CreatedBy = transaction.ToAccountId.Value,
                        ToAccountId = transaction.CreatedBy,
                        CreatedOn = DateTime.UtcNow,
                        Detail = transaction.Detail,
                        Price = sellerRevenue,
                        Fee = transaction.Fee,
                        TransactionStatus = Domain.Enums.TransactionStatusEnum.Success,
                        WalletBalance = sellerWallet.Balance,
                        PaymentMethod = "PayOS",
                        PaymentOrderCode = transaction.PaymentOrderCode,
                        PaymentTransactionId = transaction.PaymentTransactionId
                    };
                    
                    _dbContext.TransactionHistories.Add(sellerTransaction);
                }
            }
            else
            {
                // Deposit - cập nhật wallet cho buyer
                var buyerWallet = await _dbContext.Wallets
                    .FirstOrDefaultAsync(w => w.AccountId == transaction.CreatedBy);

                if (buyerWallet != null)
                {
                    // Price trong deposit là DƯƠNG (positive), cộng trực tiếp vào ví
                    buyerWallet.Balance += transaction.Price;
                    transaction.WalletBalance = buyerWallet.Balance;
                    
                    _logger.LogInformation("[PayOS Return] Updated buyer wallet. AccountId: {AccountId}, Old Balance: {OldBalance}, Amount Added: {Amount}, New Balance: {NewBalance}", 
                        transaction.CreatedBy, buyerWallet.Balance - transaction.Price, transaction.Price, buyerWallet.Balance);
                }
                else
                {
                    _logger.LogError("[PayOS Return] Buyer wallet not found for AccountId: {AccountId}", transaction.CreatedBy);
                    return false;
                }
            }

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("[PayOS Return] Successfully processed transaction {TxId}", transactionId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[PayOS Return] Error processing return for transaction {TxId}", transactionId);
            return false;
        }
    }

    public async Task<object> CancelPaymentAsync(long orderCode, string? reason = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"/v2/payment-requests/{orderCode}/cancel");
        var cancelData = new { cancellationReason = reason };
        request.Content = new StringContent(
            JsonSerializer.Serialize(cancelData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<object>(responseContent) ?? new { };
    }

    public async Task<object> GetPaymentStatusAsync(long orderCode)
    {
        var response = await _httpClient.GetAsync($"/v2/payment-requests/{orderCode}");
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<object>(responseContent) ?? new { };
    }

    private long GenerateOrderCode()
    {
        // Generate unique order code based on timestamp
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }

    private string GenerateSignature(int amount, string cancelUrl, string description, long orderCode, string returnUrl)
    {
        // PayOS signature format (from official docs): 
        // amount=$amount&cancelUrl=$cancelUrl&description=$description&orderCode=$orderCode&returnUrl=$returnUrl
        // Only these 5 fields, sorted alphabetically
        // IMPORTANT: Use the EXACT URLs including query parameters (do NOT strip them)
        
        // Build queryString exactly as PayOS expects: alphabetically sorted
        var queryString = $"amount={amount}&cancelUrl={cancelUrl}&description={description}&orderCode={orderCode}&returnUrl={returnUrl}";
        
        _logger.LogInformation("[PayOS Signature] BaseString for HMAC: {QueryString}", queryString);
        _logger.LogInformation("[PayOS Signature] ChecksumKey (first 10 chars): {Key}", _config.ChecksumKey?.Substring(0, 10) + "...");
        
        // HMAC-SHA256 with checksum key
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_config.ChecksumKey ?? ""));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(queryString));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}

// Helper classes for PayOS responses
public class PayOSPaymentResponse
{
    public string? code { get; set; }
    public string? desc { get; set; }
    public PayOSPaymentData? data { get; set; }
}

public class PayOSPaymentData
{
    public string? bin { get; set; }
    public string? accountNumber { get; set; }
    public long orderCode { get; set; }
    public int amount { get; set; }
    public string? description { get; set; }
    public string? accountName { get; set; }
    public string? currency { get; set; }
    public string? paymentLinkId { get; set; }
    public string? status { get; set; }
    public string? checkoutUrl { get; set; }
    public string? qrCode { get; set; }
}

public class PayOSWebhook
{
    public string? code { get; set; }
    public string? desc { get; set; }
    public bool success { get; set; }
    public PayOSPaymentData? data { get; set; }
    public string? signature { get; set; }
}
