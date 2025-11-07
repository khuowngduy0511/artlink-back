using Application.Commons;
using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Models;
using Domain.Repositories.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Authentication;

public class TokenHandler : ITokenHandler
{
    private readonly AppConfiguration _appConfig;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserTokenService _userTokenService;

    public TokenHandler(AppConfiguration appConfig,
        IUnitOfWork unitOfWork,
        IUserTokenService userTokenService)
    {
        _appConfig = appConfig;
        _unitOfWork = unitOfWork;
        _userTokenService = userTokenService;
    }

    public TokenModel CreateAccessToken(Account user, DateTime issueTime)
    {
        var jwtConfig = _appConfig.JwtConfiguration;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var jti = NewId.Next().ToGuid();
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, jti.ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.Audience),
            new Claim(JwtRegisteredClaimNames.Iat,issueTime.ToString("G")),
            new Claim(JwtRegisteredClaimNames.Exp,issueTime.AddHours(jwtConfig.ATExpHours).ToString("G")),
            new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var token = new JwtSecurityToken(
                claims: claims,
                notBefore: issueTime,
                expires: issueTime.AddHours(jwtConfig.ATExpHours),
                signingCredentials: credentials);
        var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenModel { Token = tokenStr, TokenId = jti };
    }

    public TokenModel CreateRefreshToken(Account user, DateTime issueTime)
    {
        var jwtConfig = _appConfig.JwtConfiguration;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var jti = NewId.Next().ToGuid();
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, jti.ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.Issuer),
            new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.Audience),
            new Claim(JwtRegisteredClaimNames.Iat,issueTime.ToString("G")),
            new Claim(JwtRegisteredClaimNames.Exp,issueTime.AddHours(jwtConfig.RTExpHours).ToString("G")),
            new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Name, user.Fullname),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var token = new JwtSecurityToken(
                claims: claims,
                notBefore: issueTime,
                expires: issueTime.AddHours(jwtConfig.RTExpHours),
                signingCredentials: credentials);
        var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenModel { Token = tokenStr, TokenId = jti };
    }

    public async Task ValidateAccessTokenAsync(TokenValidatedContext context)
    {
        var identity = context.Principal?.Identity as ClaimsIdentity;
        var jtiClaim = identity?.FindFirst(JwtRegisteredClaimNames.Jti);

        // check if token existed in db
        Guid jti;
        if (!Guid.TryParse(jtiClaim!.Value, out jti))
        {
            context.Fail("Invalid token id!");
            return;
        }
        var userToken = await _userTokenService.GetTokenByATidAsync(jti);

        if (userToken == null)
        {
            context.Fail("Invalid token! Token id does not exist.");
            return;
        }

        if (userToken.IsRevoked)
        {
            context.Fail("Invalid token! This token has been revoked.");
            return;
        }
    }

    public async Task<TokenVM?> ValidateRefreshTokenAsync(string refreshToken)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidIssuer = _appConfig.JwtConfiguration.Issuer,
                ValidAudience = _appConfig.JwtConfiguration.Audience,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_appConfig.JwtConfiguration.SecretKey))
            };
            var claimPrinciple = new JwtSecurityTokenHandler()
                    .ValidateToken(refreshToken, tokenValidationParameters, out _);

            if (claimPrinciple == null) return null;

            var jti = claimPrinciple.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;

            if (string.IsNullOrEmpty(jti)) return null;

            var userToken = await _userTokenService.GetTokenByRTidAsync(Guid.Parse(jti));
            if (userToken != null && !userToken.IsUsed && !userToken.IsRevoked)
            {
                // todo check in usual isUsed refresh token been used

                var account = await _unitOfWork.AccountRepository.GetByIdAsync(userToken.UserId);
                if (account == null) return null;
                // validate success, issue new (accessToken, refreshToken)
                userToken.IsUsed = true;
                await _userTokenService.UpdateTokenAsync(userToken);

                var issuedDate = CurrentTime.GetCurrentTime;
                var accessToken = this.CreateAccessToken(account, issuedDate);
                var newRefreshToken = this.CreateRefreshToken(account, issuedDate);

                await _userTokenService.SaveTokenAsync(new UserToken
                {
                    UserId = account.Id,
                    ATid = accessToken.TokenId,
                    AccessToken = accessToken.Token,
                    RTid = newRefreshToken.TokenId,
                    RefreshToken = newRefreshToken.Token,
                    IssuedDate = issuedDate,
                    ExpiredDate = issuedDate.AddHours(24 * 7),
                });

                return new TokenVM
                {
                    AccessToken = accessToken.Token,
                    RefreshToken = newRefreshToken.Token,
                    UserId = account.Id,
                    Username = account.Username,
                    Email = account.Email,
                    Fullname = account.Fullname,
                    Avatar = account.Avatar
                };
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error decoding token: {ex.Message}");
        }
    }

}
