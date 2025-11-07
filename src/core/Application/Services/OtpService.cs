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
}
