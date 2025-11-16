using Application.Services.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services;

public class OtpService : IOtpService
{
    private readonly IMemoryCache _memoryCache;

    public OtpService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public string? GetEmailByOTP(string key)
    {
        // Retrieve email from cache
        return _memoryCache.Get<string>(key);
    }

    public void SaveOTP(string key, string email, int expiredTime)
    {
        // Set cache expiration time to n hours
        var expiration = TimeSpan.FromMinutes(expiredTime);

        // Save OTP and email to cache
        _memoryCache.Set(key, email, expiration);
    }
    
    public bool CanSendEmail(string email, int cooldownMinutes = 1)
    {
        // Check if email has a cooldown period
        var cacheKey = $"email_cooldown_{email}";
        return !_memoryCache.TryGetValue(cacheKey, out _);
    }
    
    public void RecordEmailSent(string email, int cooldownMinutes = 1)
    {
        // Record that email was sent to prevent spam
        var cacheKey = $"email_cooldown_{email}";
        var expiration = TimeSpan.FromMinutes(cooldownMinutes);
        _memoryCache.Set(cacheKey, DateTime.UtcNow, expiration);
    }
}
