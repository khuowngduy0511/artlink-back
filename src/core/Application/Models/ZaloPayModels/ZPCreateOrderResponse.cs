namespace Application.Models.ZaloPayModels;

public class ZPCreateOrderResponse
{
    public int ReturnCode { get; set; }
    public string ReturnMessage { get; set; } = default!;
    public int SubReturnCode { get; set; }
    public string SubReturnMessage { get; set; } = default!;
    public string? ZpTransToken { get; set; }
    public string? OrderUrl { get; set; }
    public string? OrderToken { get; set; }
    public string? AppTransId { get; set; }
}
