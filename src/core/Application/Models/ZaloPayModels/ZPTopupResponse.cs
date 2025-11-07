namespace Application.Models.ZaloPayModels;

public class ZPTopupResponse
{
    public int ReturnCode { get; set; }
    public string ReturnMessage { get; set; } = default!;
    public int SubReturnCode { get; set; }
    public string SubReturnMessage { get; set; } = default!;
    public TopupData? Data { get; set; }
}

public class TopupData
{
    public string OrderId { get; set; } = default!;
    public int Status { get; set; }
    public string MUId { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public long Amount { get; set; }
    public string? Description { get; set; }
    public long PartnerFee { get; set; }
    public long ZlpFee { get; set; }
    public string? ExtraInfo { get; set; }
    public long Time { get; set; }
    public string? UpgradeUrl { get; set; }
}
