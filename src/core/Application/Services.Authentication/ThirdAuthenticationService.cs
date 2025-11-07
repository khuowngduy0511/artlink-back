using Application.Commons;
using Domain.Models;
using Google.Apis.Auth;

namespace Application.Services.Authentication;

public class ThirdAuthenticationService : IThirdAuthenticationService
{
    private readonly AppConfiguration _appConfig;

    public ThirdAuthenticationService(AppConfiguration appConfig)
    {
        _appConfig = appConfig;
    }

    public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ThirdAuthenticationModel authenticationModel)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { _appConfig.ThirdAuthentication.Google.ClientId }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(authenticationModel.IdToken, settings);
            return payload;
        }
        catch (Exception ex)
        {
            throw new ArgumentException(ex.Message);
        }
    }
}
