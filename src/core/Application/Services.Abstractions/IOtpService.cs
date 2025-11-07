namespace Application.Services.Abstractions;

public interface IOtpService
{
    void SaveOTP(string key, string email, int expiredTime);
    string? GetEmailByOTP(string key);
}
