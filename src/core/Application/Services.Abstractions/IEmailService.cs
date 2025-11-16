namespace Application.Services.Abstractions;

public interface IEmailService
{
    public Task CreateSampleMailAsync();

    public Task SendMailToViolatedAccountAsync(
        string email, string username, string violatedObject, string objectName, 
        string reason, string? detail, string resolvedDay);

    Task<bool> SendVerificationEmailAsync(string email, string verificationCode);
    
    Task SendVerificationEmailAsyncFireAndForget(string email, string verificationCode);

    public Task<bool> SendMailAsync(List<string> email, string subject, string message);
}
