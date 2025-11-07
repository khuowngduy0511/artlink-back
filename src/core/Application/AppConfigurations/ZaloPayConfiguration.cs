namespace Application.AppConfigurations;

public class ZaloPayConfiguration
{
    public string BaseUrl { get; set; } = default!;
    public int AppId { get; set; }
    public string Key1 { get; set; } = default!;
    public string Key2 { get; set; } = default!;
    public Dibursement Dibursement { get; set; } = default!;
}

public class Dibursement
{
    public int AppId { get; set; }
    public string Key1 { get; set; } = default!;
    public string PaymentId { get; set; } = default!;
    public string PrivateKey { get; set; } = default!;
}
