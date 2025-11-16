namespace Application.Services.Abstractions;

public interface IOtpService
{
    void SaveOTP(string key, string email, int expiredTime);
    string? GetEmailByOTP(string key);
    bool CanSendEmail(string email, int cooldownMinutes = 1);
    void RecordEmailSent(string email, int cooldownMinutes = 1);
}
