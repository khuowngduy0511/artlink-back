using Domain.Models;
using Google.Apis.Auth;

namespace Application.Services.Authentication;

public interface IThirdAuthenticationService
{
    Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ThirdAuthenticationModel authenticationModel);
}
