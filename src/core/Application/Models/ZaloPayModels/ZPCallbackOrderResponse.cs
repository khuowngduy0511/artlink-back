using Application.Commons;

namespace Application.Models.ZaloPayModels;

public class ZPCallbackOrderResponse
{
    public int Type { get; set; }
    public string Mac { get; set; } = default!;
    public string Data { get; set; } = default!;
    public CallbackOrderData? ToCallbackOrderData()
    {
        var result = JsonConvertHelper<CallbackOrderData>.ConvertSnakeJsonToObject(Data);
        return result;
    }
}

public class CallbackOrderData
{
    public int AppId { get; set; }
    public string AppTransId { get; set; } = default!;
    public long AppTime { get; set; }
    public string AppUser { get; set; } = default!;
    public long Amount { get; set; }
    public string EmbedData { get; set; } = default!;
    public string Item { get; set; } = default!;
    public long ZpTransId { get; set; }
    public long ServerTime { get; set; }
    public int Channel { get; set; }
    public string MerchantUserId { get; set; } = default!;
    public long UserFeeAmount { get; set; }
    public long DiscountAmount { get; set; }
}
