using Domain.Entitites;

namespace Application.Services.Abstractions;

public interface IUserTokenService
{
    Task SaveTokenAsync(UserToken userToken);
    Task<UserToken?> GetTokenByATidAsync(Guid ATid);
    Task<UserToken?> GetTokenByRTidAsync(Guid RTid);
    Task UpdateTokenAsync(UserToken userToken);
}
