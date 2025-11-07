using Application.Models.ZaloPayModels;

namespace Application.Services.Abstractions;

public interface IZaloPayService
{
    // deposit money
    public ZPCreateOrderRequest BuildZaloPayOrderCreate(OrderCreateModel orderModel);
    public Task<ZPCreateOrderResponse?> CreateOrderAsync(ZPCreateOrderRequest zaloPayOrder);
    public Task<ZPQueryOrderResponse?> QueryOrderAsync(string appTransId);
    public bool ValidateCallback(ZPCallbackOrderResponse callbackOrder);

    // withraw money
    public Task<ZPQueryUserResponse?> QueryZalopayUserAsync(UserQueryModel model);
    public Task<ZPTopupResponse?> ZalopayTopupAsync(TopupModel model, Guid transactionId);
    public Task<ZPQueryMerchantBalanceResponse?> QueryMerchantBalanceAsync();
}
