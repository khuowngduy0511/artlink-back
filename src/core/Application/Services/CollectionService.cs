using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class CollectionService : ICollectionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;

    public CollectionService(IUnitOfWork unitOfWork, IMapper mapper,
        IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _claimService = claimService;
    }

    public async Task<List<Collection>> GetAllCollectionsOfAccountAsync(Guid accountId)
    {
        return await _unitOfWork.CollectionRepository
                                .GetAllCollectionsOfAccountWithArtworksAsync(accountId);
    }

    public async Task<Collection?> GetCollectionDetailAsync(Guid collectionId)
    {
        return await _unitOfWork.CollectionRepository
                                .GetCollectionWithArtworksAsync(collectionId);
    }

    public async Task CreateCollectionAsync(Collection collection, Guid artworkId)
    {
        // BR: a collection initial must have at least 1 artwork
        collection.Bookmarks.Add(new Bookmark
        {
            CollectionId = collection.Id,
            ArtworkId = artworkId
        });
        await _unitOfWork.CollectionRepository.AddAsync(collection);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCollectionAsync(Guid collectionId, CollectionModificationModel updateModel)
    {
        var oldCollection = await _unitOfWork.CollectionRepository.GetByIdAsync(collectionId);
        if (oldCollection == null)
        {
            throw new KeyNotFoundException("Không tìm thấy bộ sưu tập.");
        }
        var currentUserId = _claimService.GetCurrentUserId;
        if (currentUserId == null || oldCollection.CreatedBy != currentUserId)
        {
            throw new UnauthorizedAccessException("Bạn không có quyền cập nhật bộ sưu tập.");
        }
        var newCollection = _mapper.Map<CollectionModificationModel, Collection>(
                                        updateModel, oldCollection);
        _unitOfWork.CollectionRepository.Update(newCollection);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCollectionAsync(Guid collectionId)
    {
        var collection = await _unitOfWork.CollectionRepository.GetByIdAsync(collectionId);
        if (collection == null)
        {
            throw new KeyNotFoundException("Không tìm thấy bộ sưu tập.");
        }
        _unitOfWork.CollectionRepository.Delete(collection);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddArtworkToCollectionAsync(Bookmark bookmark)
    {
        // check if bookmark already exist
        if (await _unitOfWork.BookmarkRepository.IsExistAsync(bookmark))
        {
            throw new BadHttpRequestException("Bạn đã lưu tác phẩm này vào bộ sưu tập này.");
        }
        var collection = await _unitOfWork.CollectionRepository.GetByIdAsync(bookmark.CollectionId);
        // check if Ids valid
        var errMsg = "";
        if (collection == null)
        {
            errMsg = "Bộ sưu tập không tồn tại.";
        }
        if (!await _unitOfWork.ArtworkRepository.IsExistedAsync(bookmark.ArtworkId))
        {
            errMsg += "Tác phẩm không tồn tại.";
        }
        if (!errMsg.IsNullOrEmpty())
        {
            throw new KeyNotFoundException(errMsg);
        }
        // check authorization
        if (_claimService.GetCurrentUserId == null
            || collection!.CreatedBy != _claimService.GetCurrentUserId)
        {
            throw new UnauthorizedAccessException("Bạn không có quyền thực hiện chức năng này.");
        }

        // add artwork to collection (add new bookmark)
        await _unitOfWork.BookmarkRepository.AddBookmarkAsync(bookmark);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveArtworkFromCollectionAsync(Bookmark bookmark)
    {
        var collection = await _unitOfWork.CollectionRepository.GetByIdAsync(bookmark.CollectionId);
        // check authorization
        if (_claimService.GetCurrentUserId == null
            || collection!.CreatedBy != _claimService.GetCurrentUserId)
        {
            throw new UnauthorizedAccessException("Bạn không có quyền thực hiện chức năng này.");
        }
        // check if bookmark exist
        var deletedBookmark = await _unitOfWork.BookmarkRepository
                .GetByIdAsync(bookmark.CollectionId, bookmark.ArtworkId);
        if (deletedBookmark == null)
        {
            throw new BadHttpRequestException("Bạn chưa lưu tác phẩm này vào bộ sưu tập này.");
        }

        // remove artwork to collection (delete bookmark)
        _unitOfWork.BookmarkRepository.DeleteBookmark(deletedBookmark);
        await _unitOfWork.SaveChangesAsync();
    }
}