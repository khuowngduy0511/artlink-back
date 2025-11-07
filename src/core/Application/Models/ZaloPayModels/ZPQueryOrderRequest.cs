namespace Application.Models.ZaloPayModels;

public class ZPQueryOrderRequest : IBaseFormRequest
{
    public int AppId { set; get; }
    public string AppTransId { set; get; } = default!;
    public string Mac { set; get; } = default!;

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
    {
        { "app_id", AppId.ToString() },
        { "app_trans_id", AppTransId },
        { "mac", Mac }
    };
    }
}
