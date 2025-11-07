using System.ComponentModel.DataAnnotations;

namespace Application.Models.ZaloPayModels;

public class UserQueryModel
{
    [Required]
    public string Phone { get; set; } = default!;
}

public class ZPQueryUserRequest : IBaseFormRequest
{
    public string RequestId { get; set; } = Guid.NewGuid().ToString();
    public int AppId { get; set; }
    public string Phone { get; set; } = default!;
    public long Time { get; set; }
    public string Mac { get; set; } = default!;

    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { "request_id", RequestId },
            { "app_id", AppId.ToString() },
            { "phone", Phone },
            { "time", Time.ToString() },
            { "mac", Mac },
        };
    }
}
