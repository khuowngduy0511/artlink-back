using System.ComponentModel.DataAnnotations;

namespace Application.Models.ZaloPayModels;

public class OrderCreateModel
{
    [Required]
    public long Amount { get; set; }
    [Required]
    public string Item { get; set; } = default!;
    public string BankCode { get; set; } = "";
    [Required]
    public string EmbedData { get; set; } = default!;
}

public class ZPCreateOrderRequest : IBaseFormRequest
{
    public int AppId { get; set; }
    public string AppUser { get; set; } = default!;
    public string AppTransId { get; set; } = default!;
    public long AppTime { get; set; }
    public long Amount { get; set; }
    public string Item { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string EmbedData { get; set; } = default!;
    public string BankCode { get; set; } = default!;
    public string Mac { get; set; } = default!;
    public string? CallbackUrl { get; set; }
    public string? DeviceInfo { get; set; }
    public string? SubAppId { get; set; }
    public string? Title { get; set; }
    public string? Currency { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { "app_id", AppId.ToString() },
            { "app_user", AppUser },
            { "app_trans_id", AppTransId },
            { "app_time", AppTime.ToString() },
            { "amount", Amount.ToString() },
            { "item", Item },
            { "description", Description },
            { "embed_data", EmbedData },
            { "bank_code", BankCode },
            { "mac", Mac },
            { "callback_url", CallbackUrl != null ? CallbackUrl : "" },
            { "device_info", DeviceInfo != null ? DeviceInfo : "" },
            { "sub_app_id", SubAppId != null ? SubAppId : "" },
            { "title", Title != null ? Title : "" },
            { "currency", Currency != null ? Currency : "" },
            { "phone", Phone != null ? Phone : "" },
            { "email", Email != null ? Email : "" },
            { "address", Address != null ? Address : "" }
        };
    }
}
