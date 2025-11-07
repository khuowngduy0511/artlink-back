
using System.ComponentModel.DataAnnotations;

namespace Application.Models.ZaloPayModels;

public class TopupModel
{
    [Required]
    public long Amount { get; set; }
    [Required]
    public string MUId { get; set; } = default!;
    [Required]
    public string Phone { get; set; } = default!;
    [Required]
    public string ReferenceId { get; set; } = default!;
}

public class ZPTopupRequest : IBaseFormRequest
{
    public int AppId { get; set; }
    public string PaymentId { get; set; } = default!;
    public string PartnerOrderId { get; set; } = default!;
    public string MUId { get; set; } = default!;
    public long Amount { get; set; }
    public string Description { get; set; } = default!;
    public string PartnerEmbedData { get; set; } = default!;
    public string ReferenceId { get; set; } = default!;
    public string ExtraInfo { get; set; } = default!;
    public long Time { get; set; }
    public string Sig { get; set; } = default!;

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { "app_id", AppId.ToString() },
            { "payment_id", PaymentId },
            { "partner_order_id", PartnerOrderId },
            { "m_u_id", MUId },
            { "amount", Amount.ToString() },
            { "description", Description },
            { "partner_embed_data", PartnerEmbedData },
            { "reference_id", ReferenceId },
            { "extra_info", ExtraInfo },
            { "time", Time.ToString() },
            { "sig", Sig },
        };
    }
}
