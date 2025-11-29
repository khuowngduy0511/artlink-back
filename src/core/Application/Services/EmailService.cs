using Application.Commons;
using Application.Services.Abstractions;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class EmailService : IEmailService
{
    private readonly AppConfiguration _appConfig;
    private readonly ILogger<EmailService> _logger;

    public EmailService(AppConfiguration appConfiguration, ILogger<EmailService> logger)
    {
        _appConfig = appConfiguration;
        _logger = logger;
    }

    public async Task CreateSampleMailAsync()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory; // Gets the base directory of the assembly
        string relativePath = Path.Combine("EmailTemplates", "BanAccountTemplate.html");
        string filePath = Path.Combine(basePath, relativePath);

        StreamReader streamreader = new(filePath);
        string mailText = streamreader.ReadToEnd();
        streamreader.Close();

        //Replace email informations
        mailText = mailText.Replace("[Username]", "Lam Lam");
        mailText = mailText.Replace("[ViolatedObject]", "Tác phẩm");
        mailText = mailText.Replace("[ObjectName]", "Tên tác phẩm");
        mailText = mailText.Replace("[Reason]", "spam");
        mailText = mailText.Replace("[ResolvedDay]", "1/1/2023");

        await SendMailAsync(new List<string> { "phuhuynh923@gmail.com" },
                "Artworkia - Nền tảng chia sẻ tác phẩm nghệ thuật", mailText);
    }

    public async Task SendMailToViolatedAccountAsync(
        string email, string username, string violatedObject, string objectName, string reason, string? detail, string resolvedDay)
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory; // Gets the base directory of the assembly
        string relativePath = Path.Combine("EmailTemplates", "BanAccountTemplate.html");
        string filePath = Path.Combine(basePath, relativePath);

        StreamReader streamreader = new(filePath);
        string mailText = streamreader.ReadToEnd();
        streamreader.Close();

        //Replace email informations
        mailText = mailText.Replace("[Username]", username);
        mailText = mailText.Replace("[ViolatedObject]", violatedObject);
        mailText = mailText.Replace("[ObjectName]", objectName);
        mailText = mailText.Replace("[Reason]", reason);
        mailText = mailText.Replace("[Detail]", detail);
        mailText = mailText.Replace("[ResolvedDay]", resolvedDay);

        await SendMailAsync(new List<string> { email },
                           "[Artlink] Tài khoản của bạn bị cấm", mailText);
    }   

    public async Task<bool> SendVerificationEmailAsync(string email, string verificationCode)
    {
        try
        {
            _logger.LogInformation("[EMAIL] Attempting to send verification email to {Email}", email);
            
            var message = @"Xác nhận địa chỉ email của bạn

Hãy chắc chắn đây là địa chỉ email đúng của bạn. Vui lòng nhập mã xác nhận này để tiếp tục đăng kí tài khoản trên hệ thống Artlink:

" + verificationCode+

@" Mã xác nhận hết hạn sau 15 phút.

Cảm ơn,
Artlink.";
            
            var result = await SendMailAsync(new List<string> { email }, "[Artlink] Xác thực email", message);
            
            if (result)
            {
                _logger.LogInformation("[EMAIL] Successfully sent verification email to {Email}", email);
            }
            else
            {
                _logger.LogWarning("[EMAIL] Failed to send verification email to {Email} - SendMailAsync returned false", email);
            }
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[EMAIL] Exception while sending verification email to {Email}: {Message}", email, ex.Message);
            return false;
        }
    }
    
    public Task SendVerificationEmailAsyncFireAndForget(string email, string verificationCode)
    {
        // Fire-and-forget: don't wait for email to be sent, return immediately
        _ = Task.Run(async () =>
        {
            try
            {
                _logger.LogInformation("[EMAIL] Starting background task to send verification email to {Email}", email);
                var success = await SendVerificationEmailAsync(email, verificationCode);
                if (!success)
                {
                    _logger.LogError("[EMAIL] Background task failed to send verification email to {Email}", email);
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw (fire-and-forget pattern)
                _logger.LogError(ex, "[EMAIL ERROR] Background task exception while sending verification email to {Email}: {Message}", email, ex.Message);
            }
        });
        
        // Return completed task immediately - email is being sent in background
        _logger.LogInformation("[EMAIL] Fire-and-forget task initiated for {Email}", email);
        return Task.CompletedTask;
    }
    public async Task<bool> SendMailAsync(List<string> emails, string subject, string message)
    {
        try
        {
            var email = _appConfig.EmailSetting.Email;
            var password = _appConfig.EmailSetting.Password;
            var dispayName = _appConfig.EmailSetting.DisplayName;
            
            _logger.LogInformation("[EMAIL] Preparing to send email. From: {From}, To: {Recipients}, Subject: {Subject}",
                email, string.Join(", ", emails), subject);
            
            // Validate email configuration
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                _logger.LogError("[EMAIL] Email configuration is incomplete. Email: {Email}, Password: {HasPassword}",
                    email, !string.IsNullOrWhiteSpace(password));
                return false;
            }
            
            MailMessage myMessage = new();
            foreach (var mail in emails)
            {
                myMessage.To.Add(mail);
            }
            myMessage.IsBodyHtml = true;
            myMessage.From = new MailAddress(email, dispayName);
            myMessage.Subject = subject;
            myMessage.Body = message;
            
            _logger.LogInformation("[EMAIL] Connecting to SMTP server: smtp.gmail.com:465 (SSL enabled)");
            
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 465; // Changed from 587 to 465 (SSL instead of TLS) - Render blocks port 587
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(email, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                // Add timeout to prevent long waits (10 seconds)
                smtp.Timeout = 10000;
                
                _logger.LogInformation("[EMAIL] Sending email via SMTP...");
                // Send email asynchronously with timeout protection
                await smtp.SendMailAsync(myMessage);
                _logger.LogInformation("[EMAIL] Email sent successfully to {Recipients}", string.Join(", ", emails));
            }
            return true;
        }
        catch (SmtpException smtpEx)
        {
            _logger.LogError(smtpEx, "[EMAIL] SMTP Error: {StatusCode} - {Message}. InnerException: {InnerMessage}",
                smtpEx.StatusCode, smtpEx.Message, smtpEx.InnerException?.Message ?? "None");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[EMAIL] Unexpected error while sending email: {Message}. InnerException: {InnerMessage}",
                ex.Message, ex.InnerException?.Message ?? "None");
            return false;
        }
    }
}
