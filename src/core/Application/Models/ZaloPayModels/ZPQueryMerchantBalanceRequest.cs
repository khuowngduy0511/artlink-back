
namespace Application.Models.ZaloPayModels;

public class ZPQueryMerchantBalanceRequest : IBaseFormRequest
{
    public string RequestId { get; set; } = default!;
    public int AppId { get; set; }
    public string PaymentId { get; set; } = default!;
    public long Time { get; set; }
    public string Mac { get; set; } = default!;

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { "request_id", RequestId },
            { "app_id", AppId.ToString() },
            { "payment_id", PaymentId },
            { "time", Time.ToString() },
            { "mac", Mac },
        };
    }
}
