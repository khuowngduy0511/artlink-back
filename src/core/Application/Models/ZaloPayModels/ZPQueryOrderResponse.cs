namespace Application.Models.ZaloPayModels;

public class ZPQueryOrderResponse
{
    public int ReturnCode { get; set; }
    public string ReturnMessage { get; set; } = default!;
    public int SubReturnCode { get; set; }
    public string SubReturnMessage { get; set; } = default!;
    public bool IsProcessing { get; set; }
    public long Amount { get; set; }
    public long ZpTransId { get; set; }
}
