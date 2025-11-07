using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class UserTokenService : IUserTokenService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserTokenService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserToken?> GetTokenByATidAsync(Guid ATid)
        => await _unitOfWork.UserTokenRepository.GetSingleByConditionAsync(x => x.ATid == ATid);

    public async Task<UserToken?> GetTokenByRTidAsync(Guid RTid)
        => await _unitOfWork.UserTokenRepository.GetSingleByConditionAsync(x => x.RTid == RTid);

    public async Task SaveTokenAsync(UserToken userToken)
    {
        await _unitOfWork.UserTokenRepository.AddAsync(userToken);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateTokenAsync(UserToken userToken)
    {
        _unitOfWork.UserTokenRepository.Update(userToken);
        await _unitOfWork.SaveChangesAsync();
    }
}
