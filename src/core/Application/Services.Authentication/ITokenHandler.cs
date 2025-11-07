using Domain.Entitites;
using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Application.Services.Authentication;

public interface ITokenHandler
{
    Task ValidateAccessTokenAsync(TokenValidatedContext context);
    Task<TokenVM?> ValidateRefreshTokenAsync(string RefreshToken);
    TokenModel CreateRefreshToken(Account user, DateTime issueTime);
    TokenModel CreateAccessToken(Account user, DateTime issueTime);
}
