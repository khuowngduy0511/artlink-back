namespace Application.Models.ZaloPayModels;

public class ZPQueryMerchantBalanceResponse
{
    public int ReturnCode { get; set; }
    public string ReturnMessage { get; set; } = default!;
    public int SubReturnCode { get; set; }
    public string SubReturnMessage { get; set; } = default!;
    public MerchanBalanceData? Data { get; set; }
}

public class MerchanBalanceData
{
    public long Balance { get; set; }
}

