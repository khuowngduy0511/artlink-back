using Application.Commons;
using Application.Models;
using Application.Services.Abstractions;
using Application.Services.Authentication;
using AutoMapper;
using Domain.Entitites;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IUserTokenService _userTokenService;
    private readonly ITokenHandler _tokenHandler;
    private readonly IMapper _mapper;
    private readonly IThirdAuthenticationService _thirdAuthenticationService;
    private readonly AppConfiguration _appConfiguration;
    private readonly IClaimService _claimService;
    private readonly IOtpService _otpService;
    private readonly IEmailService _emailService;

    public AuthController(IAccountService accountService,
        IUserTokenService userTokenService,
        ITokenHandler tokenHandler,
        IMapper mapper,
        IThirdAuthenticationService thirdAuthenticationService,
        AppConfiguration appConfiguration,
        IClaimService claimService,
        IOtpService otpService,
        IEmailService emailService)
    {
        _accountService = accountService;
        _userTokenService = userTokenService;
        _tokenHandler = tokenHandler;
        _mapper = mapper;
        _thirdAuthenticationService = thirdAuthenticationService;
        _appConfiguration = appConfiguration;
        _claimService = claimService;
        _otpService = otpService;
        _emailService = emailService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if (loginModel == null)
        {
            return BadRequest(new ApiResponse
            {
                IsSuccess = false,
                ErrorMessage = "Thông tin không hợp lệ!"
            });
        }
        // check whether account exist in db
        var account = await _accountService.CheckLoginAsync(loginModel.Username, loginModel.Password);
        if (account == null)
        {
            return Unauthorized(new ApiResponse
            {
                IsSuccess = false,
                ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng."
            });
        }
        if (account.DeletedOn != null)
        {
            return Unauthorized(new ApiResponse
            {
                IsSuccess = false,
                ErrorMessage = "Tài khoản của bạn đã bị vô hiệu hóa. " +
                "Thông tin chi tiết vui lòng kiểm tra email"
            });
        }
        // login success - issue (access token, refresh token) pair
        var issuedDate = CurrentTime.GetCurrentTime;
        var accessToken = _tokenHandler.CreateAccessToken(account, issuedDate);
        var refreshToken = _tokenHandler.CreateRefreshToken(account, issuedDate);
        var token = new UserToken
        {
            UserId = account.Id,
            ATid = accessToken.TokenId,
            AccessToken = accessToken.Token,
            RTid = refreshToken.TokenId,
            RefreshToken = refreshToken.Token,
            IssuedDate = issuedDate,
            ExpiredDate = issuedDate.AddHours(_appConfiguration.JwtConfiguration.RTExpHours),
        };
        await _userTokenService.SaveTokenAsync(token);

        return Ok(new TokenVM
        {
            AccessToken = token.AccessToken,
            RefreshToken = token.RefreshToken,
            UserId = account.Id,
            Username = account.Username,
            Email = account.Email,
            Fullname = account.Fullname,
            Avatar = account.Avatar,
        });
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
    {
        try
        {
            var tokenModel = await _tokenHandler.ValidateRefreshTokenAsync(model.RefreshToken);
            if (tokenModel == null)
            {
                return BadRequest(new ApiResponse { ErrorMessage = "Refresh token không hợp lệ." });
            }
            return Ok(tokenModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var ATid = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);
        if (ATid == null)
        {
            return Unauthorized(new ApiResponse { ErrorMessage = "Token không hợp lệ." });
        }
        var userToken = await _userTokenService.GetTokenByATidAsync(Guid.Parse(ATid.Value));
        if (userToken == null)
        {
            return BadRequest(new ApiResponse { ErrorMessage = "Không tìm thấy Access token" });
        }
        // token valid -> update in db
        userToken.IsRevoked = true;
        await _userTokenService.UpdateTokenAsync(userToken);

        return Ok(new ApiResponse { IsSuccess = true });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (model == null)
        {
            return BadRequest(new ApiResponse { ErrorMessage = "Thông tin không hợp lệ." });
        }
        try
        {
            await _accountService.CreateAccountAsync(_mapper.Map<Account>(model));
            return Ok(new ApiResponse { IsSuccess = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }

    }

    [HttpPost("login-google")]
    public async Task<IActionResult> LoginWithGoogle([FromBody] ThirdAuthenticationModel externalAuthDto)
    {
        try
        {
            var payload = await _thirdAuthenticationService.VerifyGoogleToken(externalAuthDto);
            if (payload == null)
            {
                return BadRequest(new ApiResponse { ErrorMessage = "Xác thực bên ngoài (payload) không hợp lệ." });
            }
            // verify gg successful
            var account = await _accountService.GetAccountByEmailAsync(payload.Email);
            // if email not exist in system yet -> create new
            if (account == null)
            {
                account = new Account
                {
                    Email = payload.Email,
                    Fullname = payload.Name,
                    Username = payload.Email,
                    Avatar = payload.Picture
                };
                await _accountService.CreateAccountAsync(account);
            } else if (account.DeletedOn != null)
            {
                return Unauthorized(new ApiResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Tài khoản của bạn đã bị vô hiệu hóa. " +
                                    "Thông tin chi tiết vui lòng kiểm tra email"
                });
            }

            // login success - issue (access token, refresh token) pair
            var issuedDate = CurrentTime.GetCurrentTime;
            var accessToken = _tokenHandler.CreateAccessToken(account, issuedDate);
            var refreshToken = _tokenHandler.CreateRefreshToken(account, issuedDate);
            var token = new UserToken
            {
                UserId = account.Id,
                ATid = accessToken.TokenId,
                AccessToken = accessToken.Token,
                RTid = refreshToken.TokenId,
                RefreshToken = refreshToken.Token,
                IssuedDate = issuedDate,
                ExpiredDate = issuedDate.AddHours(24 * 7),
            };
            await _userTokenService.SaveTokenAsync(token);

            return Ok(new TokenVM
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                UserId = account.Id,
                Username = account.Username,
                Email = account.Email,
                Fullname = account.Fullname,
                Avatar = account.Avatar
            });
        }catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        } catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("validate-access-token")]
    [Authorize]
    public IActionResult ValidateAccessToken()
    {
        return Ok(new
        {
            UserId = _claimService.GetCurrentUserId,
            UserName = _claimService.GetCurrentUserName,
            Role = _claimService.GetCurrentRole
        });
    }

    [HttpPost("send-email-verification")]
    public async Task<IActionResult> SendEmailVerification(EmailModel model)
    {
        // Generate a random 6-digit verification code
        var random = new Random();
        var code = random.Next(100000, 999999).ToString();
        // Save otp key with email in memory cache
        _otpService.SaveOTP(code, model.Email, 15); // otp expired in 15 minutes

        // Send verification code to email
        var result = await _emailService.SendVerificationEmailAsync(model.Email, code);
        if (!result)
        {
            return BadRequest(new {ErrorMessage = $"Không thể gửi mã xác thực đến email {model.Email}." });
        }
        return Ok(new {Message = $"Đã gửi mã xác thực đến email {model.Email}, vui lòng kiểm tra email."});
    }

    [HttpPost("verify-email")]
    public IActionResult VerifyEmail(EmailVerificationModel model)
    {
        var email = _otpService.GetEmailByOTP(model.VerificationCode);
        if (email == null || !email.Equals(model.Email)) {
            return BadRequest(new {ErrorMessage = $"Xác thực thất bại! Mã xác thực không hợp lệ."});
        }
        return Ok(new { Message = $"Xác thực email {model.Email} thành công." });
    }
}