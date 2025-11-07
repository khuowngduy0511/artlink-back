namespace Domain.Models;

public class TokenVM
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public Guid UserId { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Fullname { get; set; } = default!;
    public string? Avatar { get; set; }
}

public class TokenModel
{
    public string Token { get; set; } = default!;
    public Guid TokenId { get; set; }
}
