using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Services;
public class LikeService : ILikeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public LikeService(
        IUnitOfWork unitOfWork,
        IClaimService claimService,
        INotificationService notificationService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
        _notificationService = notificationService;
        _mapper = mapper;
    }

    public async Task CreateLikeAsync(LikeModel likeModel)
    {
        var currentUserId = _claimService.GetCurrentUserId ?? default;
        var currentUsername = _claimService.GetCurrentUserName ?? default;

        var tmpLike = await _unitOfWork.LikeRepository.GetByIdAsync(currentUserId, likeModel.ArtworkId);
        if (tmpLike != null)
        {
            throw new BadHttpRequestException("Bạn đã thích tác phẩm này.");
        }

        Like like = new()
        {
            AccountId = currentUserId,
            ArtworkId = likeModel.ArtworkId
        };
        await _unitOfWork.LikeRepository.AddLikeAsync(like);
        var artwork = await _unitOfWork.ArtworkRepository.GetByIdAsync(like.ArtworkId);
        if (artwork == null)
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        artwork.LikeCount++;
        _unitOfWork.ArtworkRepository.Update(artwork);

        await _unitOfWork.SaveChangesAsync();

        // add new notification
        var notification = new NotificationModel
        {
            SentToAccount = artwork.CreatedBy!.Value,
            Content = $"Người dùng [{currentUsername}] thích tác phẩm [{artwork.Title}]",
            NotifyType = NotifyTypeEnum.Information,
            ReferencedAccountId = currentUserId,
            ReferencedArtworkId = artwork.Id
        };
        await _notificationService.AddNotificationAsync(notification);
    }

    public async Task DeleteLikeAsync(Guid artworkId)
    {
        Guid accountId = _claimService.GetCurrentUserId ?? default;
        Console.WriteLine($"[DeleteLikeAsync] AccountId: {accountId}, ArtworkId: {artworkId}");

        var artwork = await _unitOfWork.ArtworkRepository.GetByIdAsync(artworkId);
        if (artwork == null)
        {
            Console.WriteLine($"[DeleteLikeAsync] Artwork not found: {artworkId}");
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        }

        var tmpLike = await _unitOfWork.LikeRepository.GetByIdAsync(accountId, artworkId);
        if (tmpLike == null)
        {
            Console.WriteLine($"[DeleteLikeAsync] Like not found for AccountId: {accountId}, ArtworkId: {artworkId}");
            throw new BadHttpRequestException("Bạn chưa thích tác phẩm này.");
        }

        Console.WriteLine($"[DeleteLikeAsync] Deleting like for AccountId: {accountId}, ArtworkId: {artworkId}");
        // hard delete like in db
        _unitOfWork.LikeRepository.DeleteLike(tmpLike);
        artwork.LikeCount--;
        _unitOfWork.ArtworkRepository.Update(artwork);
        await _unitOfWork.SaveChangesAsync();
        Console.WriteLine($"[DeleteLikeAsync] Successfully deleted like and updated artwork");
    }

    public async Task<ArtworkLikeVM> GetAllLikesOfAccountAsync(Guid accountId)
    {
        bool isAccountExisted = await _unitOfWork.AccountRepository.IsExistedAsync(accountId);
        if (!isAccountExisted)
            throw new KeyNotFoundException("Không tìm thấy tài khoản.");

        var listAccounts = await _unitOfWork.LikeRepository.GetAllLikeOfAccountAsync(accountId);
        var listArtworksLikeds = new List<ArtworkPreviewVM>();
        foreach (var like in listAccounts)
        {
            listArtworksLikeds.Add(_mapper.Map<ArtworkPreviewVM>(like.Artwork));
        }
        ArtworkLikeVM artworkLikeVM = new()
        {
            AccountId = accountId,
            ArtworkLikeds = listArtworksLikeds
        };
        return artworkLikeVM;
    }

    public async Task<AccountLikeVM> GetAllLikesOfArtworkAsync(Guid artworkId)
    {
        bool isArtworkExisted = await _unitOfWork.ArtworkRepository.IsExistedAsync(artworkId);
        if (!isArtworkExisted)
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");

        var listLikes = await _unitOfWork.LikeRepository.GetAllLikeOfArtworkAsync(artworkId);
        var listAccountsLikeds = new List<AccountBasicInfoVM>();
        foreach (var like in listLikes)
        {
            listAccountsLikeds.Add(_mapper.Map<AccountBasicInfoVM>(like.Account));
        }
        AccountLikeVM accountLikeVM = new()
        {
            ArtworkId = artworkId,
            AccountLikeds = listAccountsLikeds
        };
        return accountLikeVM;
    }
    public Task<Like?> GetLikeByIdAsync(Guid accountId, Guid artworkId)
     => _unitOfWork.LikeRepository.GetByIdAsync(accountId, artworkId);

    public async Task<IsLikedVM> GetIsLikedArtworkAsync(Guid accountId, Guid artworkId)
    {
        bool isArtworkExisted = await _unitOfWork.ArtworkRepository.IsExistedAsync(artworkId);
        if (!isArtworkExisted)
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        var likeObj = await _unitOfWork.LikeRepository.GetByIdAsync(accountId, artworkId);
        return new IsLikedVM
        {
            ArtworkId = artworkId,
            IsLiked = likeObj != null
        };
    }

    public async Task<List<IsLikedVM>> GetListIsLikedArtworkAsync(Guid accountId, List<Guid> artworkIds)
    {
        List<IsLikedVM> listIsLiked = new();
        foreach (var artworkId in artworkIds)
        {
            bool isArtworkExisted = await _unitOfWork.ArtworkRepository.IsExistedAsync(artworkId);
            if (!isArtworkExisted)
                throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
            var likeObj = await _unitOfWork.LikeRepository.GetByIdAsync(accountId, artworkId);
            listIsLiked.Add(new IsLikedVM
            {
                ArtworkId = artworkId,
                IsLiked = likeObj != null
            });
        }
        return listIsLiked;

    }
}
