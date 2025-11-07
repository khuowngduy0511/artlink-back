using Newtonsoft.Json;

namespace Application.Models.ZaloPayModels;

public class ZPQueryUserResponse
{
    public int ReturnCode { get; set; }
    public string ReturnMessage { get; set; } = default!;
    public int SubReturnCode { get; set; }
    public string SubReturnMessage { get; set; } = default!;
    public UserQueryData? Data { get; set; }
}

public class UserQueryData
{
    public string? ReferenceId { get; set; }
    [JsonProperty("m_u_id")]
    public string? MUId { get; set; }
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? OnboardingUrl { get; set; }
}
