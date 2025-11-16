using Application.Commons;
using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Nest;
using static Application.Commons.VietnameseEnum;

namespace Application.Services;

public class ArtworkService : IArtworkService
{
    private static readonly string THUMBNAIL_ARTWORK_FOLDER = "ThumbnailArtwork";
    private static readonly string ASSET_FOLDER = "Asset";
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;
    private readonly ITagDetailService _tagDetailService;
    private readonly ISoftwareDetailService _softwareDetailService;
    private readonly INotificationService _notificationService;
    private readonly IElasticClient? _elasticClient;
    private readonly ICategoryArtworkDetailService _categoryArtworkDetailService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;
    public ArtworkService(
        IUnitOfWork unitOfWork,
        IImageService imageService,
        ITagDetailService tagDetailService,
        ISoftwareDetailService softwareDetailService,
        INotificationService notificationService,
        IElasticClient? elasticClient,
        ICategoryArtworkDetailService catworkDetailService,
        ICloudinaryService cloudinaryService,
        IClaimService claimService,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _imageService = imageService;
        _tagDetailService = tagDetailService;
        _softwareDetailService = softwareDetailService;
        _notificationService = notificationService;
        _elasticClient = elasticClient;
        _categoryArtworkDetailService = catworkDetailService;
        _cloudinaryService = cloudinaryService;
        _claimService = claimService;
        _mapper = mapper;
    }

    public async Task<List<ArtworkVM>> GetAllArtworksAsync()
    {
        var listArtwork = await _unitOfWork.ArtworkRepository.GetAllArtworks();
        var listArtworkVM = _mapper.Map<List<ArtworkVM>>(listArtwork);
        return listArtworkVM;
    }

    public async Task<IPagedList<ArtworkPreviewVM>> GetArtworksAsync(ArtworkCriteria criteria)
    {
        Guid? loginId = _claimService.GetCurrentUserId;

        var listArtwork = await _unitOfWork.ArtworkRepository.GetArtworksAsync(
            criteria.Keyword, criteria.SortColumn,
            criteria.SortOrder, criteria.PageNumber, criteria.PageSize,
            null, criteria.CategoryId, criteria.TagId);
        var listArtworkPreviewVM = _mapper.Map<PagedList<ArtworkPreviewVM>>(listArtwork);

        // check if user login liked artworks
        if (loginId != null)
        {
            foreach (var artworkPreviewVM in listArtworkPreviewVM.Items)
            {
                var isLiked = await _unitOfWork.LikeRepository.GetByIdAsync(loginId.Value, artworkPreviewVM.Id);
                artworkPreviewVM.IsLiked = isLiked != null;
            }
        }

        return listArtworkPreviewVM;
    }

    public async Task<IPagedList<ArtworksV2>> SearchArtworksWithElasticSearchAsync(ArtworkElasticCriteria criteria)
    {
        Guid? loginId = _claimService.GetCurrentUserId;

        var boolQuery = new BoolQuery
        {
            MustNot = new List<QueryContainer>
            {
                new ExistsQuery
                {
                    Field = "deletedOn"
                }
            },
            Filter = new List<QueryContainer>
            {
                new TermQuery
                {
                    Field = "state",
                    Value = "accepted"
                },
                new TermQuery
                {
                    Field = "privacy",
                    Value = "public"
                }
            }
        };

        // Only add keyword search if keyword is provided
        if (!string.IsNullOrWhiteSpace(criteria.Keyword))
        {
            boolQuery.Should = new List<QueryContainer>
            {
                // Keyword search in title and account fields
                new MultiMatchQuery
                {
                    Fields = new[] { "title", "account.email", "account.fullname", "account.username" },
                    Query = criteria.Keyword
                },
                // Nested query for tags - search in tagName field
                new NestedQuery
                {
                    Path = "tagList",
                    Query = new MatchQuery
                    {
                        Field = "tagList.tagName",
                        Query = criteria.Keyword,
                        Operator = Operator.Or,
                        Fuzziness = Fuzziness.Auto
                    }
                },
                // Nested query for categories - search in categoryName field
                new NestedQuery
                {
                    Path = "categoryList",
                    Query = new MatchQuery
                    {
                        Field = "categoryList.categoryName",
                        Query = criteria.Keyword,
                        Operator = Operator.Or,
                        Fuzziness = Fuzziness.Auto
                    }
                }
            };
            // Minimum should match: at least one should clause must match
            boolQuery.MinimumShouldMatch = 1;
        }

        var searchRequest = new SearchRequest<ArtworksV2>
        {
            From = (criteria.PageNumber - 1) * criteria.PageSize,
            Size = criteria.PageSize,
            Query = boolQuery,
            Sort = new List<ISort>
            {
                new FieldSort
                {
                    Field = criteria.SortColumn ?? "_score",
                    Order = criteria.SortOrder == "asc" ? SortOrder.Ascending : SortOrder.Descending
                }
            },
            Explain = true
        };
        if (criteria.CategoryId != null)
        {
            searchRequest.Query &= (
                new BoolQuery
                {
                    Must = new List<QueryContainer>
                    {
                        new NestedQuery
                        {
                            Path = "categoryList",
                            Query = new TermQuery
                            {
                                Field = "categoryList.id",
                                Value = criteria.CategoryId
                            }
                        }
                    }
                }
            );
        }

        if (criteria.IsAssetAvailable != null && criteria.IsAssetAvailable.Value)
        {
            searchRequest.Query &= (
                new BoolQuery
                {
                    Must = new List<QueryContainer>
                    {
                        new NestedQuery
                        {
                            Path = "assets",
                            Query = new ExistsQuery
                            {
                                Field = "assets",
                            }
                        }
                    }
                }
            );

            if (criteria.IsAssetFree != null && criteria.IsAssetFree.Value)
            {
                searchRequest.Query &= (
                    new BoolQuery
                    {
                        Must = new List<QueryContainer>
                        {
                            new NestedQuery
                            {
                                Path = "assets",
                                Query = new TermQuery
                                {
                                    Field = "assets.price",
                                    Value = 0
                                }
                            }
                        }
                    }
                );
            }
        }

        // Elasticsearch is disabled - use SQL search endpoint instead
        if (_elasticClient == null)
        {
            throw new InvalidOperationException("Elasticsearch is not configured. Please use SQL search endpoint (/api/artworks) instead.");
        }

        var result2 = await _elasticClient.SearchAsync<ArtworksV2>(searchRequest);
        var searchArtworks = result2.Documents.ToList();
        // check if user login liked artworks
        if (loginId != null)
        {
            foreach (var searchArtwork in searchArtworks)
            {
                var isLiked = await _unitOfWork.LikeRepository.GetByIdAsync(loginId.Value, searchArtwork.Id);
                searchArtwork.IsLiked = isLiked != null;
            }
        }

        PagedList<ArtworksV2> pagedList = new(
            searchArtworks,
            (int)result2.Total,
            criteria.PageNumber,
            criteria.PageSize
            );
        return pagedList;

    }

    public async Task<IPagedList<ArtworksV2>> GetRecommenedArtworkAsync(RecommendedArtworkCriteria criteria)
    {
        Guid? loginId = _claimService.GetCurrentUserId;
        var searchRequest = new SearchRequest<ArtworksV2>();
        if (criteria.ArtworkIds == null || criteria.ArtworkIds.Count == 0)
        {
            searchRequest = new SearchRequest<ArtworksV2>
            {
                From = (criteria.PageNumber - 1) * criteria.PageSize,
                Size = criteria.PageSize,
                Query = new BoolQuery
                {
                    MustNot = new List<QueryContainer>
                    {
                        new ExistsQuery
                        {
                            Field = "deletedOn"
                        }
                    },
                    Filter = new List<QueryContainer>
                    {
                        new TermQuery
                        {
                            Field = "state",
                            Value = "accepted"
                        },
                        new TermQuery
                        {
                            Field = "privacy",
                            Value = "public"
                        }
                    },
                },

                Sort = new List<ISort>
                {
                    new FieldSort
                    {
                        Field = "viewCount",
                        Order = SortOrder.Descending
                    }
                }
            };
        }
        else
        {
            var listLikes = criteria.ArtworkIds.Select(id =>
            {
                Nest.Like like = new LikeDocument<ArtworksV2>(id);
                return like;
            }).ToList();

            searchRequest = new SearchRequest<ArtworksV2>
            {
                From = (criteria.PageNumber - 1) * criteria.PageSize,
                Size = criteria.PageSize,
                Query = new BoolQuery
                {
                    MustNot = new List<QueryContainer>
                    {
                        new ExistsQuery
                        {
                            Field = "deletedOn"
                        }
                    },

                    Filter = new List<QueryContainer>
                    {
                        new TermQuery
                        {
                            Field = "state",
                            Value = "accepted"
                        },
                        new TermQuery
                        {
                            Field = "privacy",
                            Value = "public"
                        }
                    },
                    Should = new List<QueryContainer>
                    {
                        new NestedQuery
                        {
                            Path = "tagList",
                            Query = new QueryContainer(
                                new MoreLikeThisQuery
                                {
                                    Fields = new[] { "tagList.tagName" },
                                    Boost = 1.3,
                                    Like = listLikes,
                                    MinTermFrequency = 1,
                                    MinDocumentFrequency = 5,
                                    MaxQueryTerms = 20,
                                    StopWords = new [] { "AI"}
                                }
                            )
                        },
                        new NestedQuery
                        {
                            Path = "categoryList",
                            Query = new QueryContainer(
                                new MoreLikeThisQuery
                                {
                                    Fields = new[] { "categoryList.categoryName" },
                                    Boost = 1,
                                    Like = listLikes,
                                    MinTermFrequency = 1,
                                    MinDocumentFrequency = 5,
                                    MaxQueryTerms = 20
                                }
                            )
                        },
                        new MoreLikeThisQuery
                        {
                            Fields = new[] { "title", "account.username"},
                            Boost = 1.5,
                            Like = listLikes,
                            MinTermFrequency = 1,
                            MinDocumentFrequency = 5,
                            MaxQueryTerms = 20
                        },
                        new MatchAllQuery()
                    }
                },
                Sort = new List<ISort>
                {
                    new FieldSort
                    {
                        Field = "_score",
                        Order = SortOrder.Descending
                    }
                }
            };
        }

        // Elasticsearch is disabled - use SQL search endpoint instead
        if (_elasticClient == null)
        {
            throw new InvalidOperationException("Elasticsearch is not configured. Please use SQL search endpoint (/api/artworks) instead.");
        }

        var result = await _elasticClient.SearchAsync<ArtworksV2>(searchRequest);
        var searchArtworks = result.Documents.ToList();
        // check if user login liked artworks
        if (loginId != null)
        {
            foreach (var searchArtwork in searchArtworks)
            {
                var isLiked = _unitOfWork.LikeRepository.GetByIdAsync(loginId.Value, searchArtwork.Id).Result;
                searchArtwork.IsLiked = isLiked != null;
            }
        }

        PagedList<ArtworksV2> pagedList = new(
            searchArtworks,
            (int)result.Total,
            criteria.PageNumber,
            criteria.PageSize
        );
        return pagedList;

    }

    public async Task<IPagedList<ArtworkModerationVM>> GetAllArtworksForModerationAsync(ArtworkModerationCriteria criteria)
    {
        var listArtwork = await _unitOfWork.ArtworkRepository.GetArtworksForModerationAsync(
            criteria.Keyword, criteria.SortColumn,
            criteria.SortOrder, criteria.PageNumber, criteria.PageSize,
            null, criteria.CategoryId, criteria.TagId, criteria.State, criteria.Privacy);
        var listArtworkModerationVM = _mapper.Map<PagedList<ArtworkModerationVM>>(listArtwork);
        return listArtworkModerationVM;
    }

    public async Task<IPagedList<ArtworkPreviewVM>> GetAllArtworksByAccountIdAsync(Guid accountId, ArtworkModerationCriteria criteria)
    {
        Guid? loginId = _claimService.GetCurrentUserId;

        bool isAccountExist = await _unitOfWork.AccountRepository.IsExistedAsync(accountId);
        if (!isAccountExist)
            throw new KeyNotFoundException("Không tìm thấy tài khoản.");

        var listArtwork = await _unitOfWork.ArtworkRepository.GetArtworksAsync(
            criteria.Keyword, criteria.SortColumn,
            criteria.SortOrder, criteria.PageNumber, criteria.PageSize,
            accountId, criteria.CategoryId, criteria.TagId, criteria.State, criteria.Privacy);
        var listArtworkPreviewVM = _mapper.Map<PagedList<ArtworkPreviewVM>>(listArtwork);

        // check if user login liked artworks
        if (loginId != null)
        {
            foreach (var artworkPreviewVM in listArtworkPreviewVM.Items)
            {
                var isLiked = await _unitOfWork.LikeRepository.GetByIdAsync(loginId.Value, artworkPreviewVM.Id);
                artworkPreviewVM.IsLiked = isLiked != null;
            }
        }
        return listArtworkPreviewVM;
    }

    public async Task<IPagedList<ArtworkPreviewVM>> GetArtworksOfFollowingsAsync(PagedCriteria criteria)
    {
        Guid followerId = _claimService.GetCurrentUserId ?? default;
        var listArtwork = await _unitOfWork.ArtworkRepository.GetArtworksOfFollowingsAsync(
            followerId, criteria.PageNumber, criteria.PageSize);
        var listArtworkPreviewVM = _mapper.Map<PagedList<ArtworkPreviewVM>>(listArtwork);

        // check if user liked artworks
        foreach (var artworkPreviewVM in listArtworkPreviewVM.Items)
        {
            var isLiked = await _unitOfWork.LikeRepository.GetByIdAsync(followerId, artworkPreviewVM.Id);
            artworkPreviewVM.IsLiked = isLiked != null;
        }

        return listArtworkPreviewVM;
    }

    public async Task<IPagedList<ArtworkContainAssetVM>> GetArtworksContainAssetsOfAccountAsync(Guid accountId, PagedCriteria criteria)
    {
        Guid? loginId = _claimService.GetCurrentUserId;

        var listArtwork = await _unitOfWork.ArtworkRepository.GetArtworksContainAssetsOfAccountAsync(
            accountId, criteria.PageNumber, criteria.PageSize);
        var pagedListArtworkVM = _mapper.Map<PagedList<ArtworkContainAssetVM>>(listArtwork);

        if (loginId != null)
        {
            foreach (var artworkVM in pagedListArtworkVM.Items)
            {
                if (artworkVM.Assets != null)
                {
                    foreach (var asset in artworkVM.Assets)
                    {
                        var isBought = await _unitOfWork.TransactionHistoryRepository.GetAssetTransactionAsync(loginId.Value, asset.Id);
                        asset.IsBought = isBought != null;
                    }
                }
            }
        }


        return pagedListArtworkVM;
    }

    public async Task<List<ImageDuplicationVM>> GetArtworksDuplicateAsync(Guid artworkId)
    {
        var isExisted = await _unitOfWork.ArtworkRepository.IsExistedAsync(artworkId);
        if (!isExisted)
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        var listArtwork = await _unitOfWork.ArtworkRepository.GetArtworksDuplicateAsync(artworkId);
        var listArtworkVM = _mapper.Map<List<ImageDuplicationVM>>(listArtwork);
        return listArtworkVM;
    }

    public async Task<ArtworkVM?> GetArtworkByIdAsync(Guid artworkId)
    {
        Guid? accountId = _claimService.GetCurrentUserId;
        bool isModeratorOrAdmin = _claimService.IsModeratorOrAdmin();

        var artwork = await _unitOfWork.ArtworkRepository.GetArtworkDetailByIdAsync(artworkId)
            ?? throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        if (artwork.DeletedOn != null)
            throw new KeyNotFoundException("Tác phẩm đã bị xóa.");

        var artworkVM = _mapper.Map<ArtworkVM>(artwork);

        // Admin/Moderator can view artwork with any state
        if (artwork.State != StateEnum.Accepted && accountId != artwork.CreatedBy && !isModeratorOrAdmin)
            throw new UnauthorizedAccessException("Tác phẩm này chưa được chấp nhận.");
        if (artwork.Privacy == PrivacyEnum.Private && accountId != artwork.CreatedBy && !isModeratorOrAdmin)
            throw new UnauthorizedAccessException("Tác phẩm này là tác phẩm riêng tư.");

        // check if user is logged in
        if (accountId != null)
        {
            // check if account is blocking or blocked
            if (await _unitOfWork.BlockRepository.IsBlockedOrBlockingAsync(accountId.Value, artworkVM.CreatedBy!.Value))
            {
                throw new BadHttpRequestException("Không tìm thấy tác phẩm vì chặn hoặc bị chặn.");
            }
            if (artworkVM.Assets != null)
            {
                foreach (var asset in artworkVM.Assets)
                {
                    var isBought = await _unitOfWork.TransactionHistoryRepository.GetAssetTransactionAsync(accountId.Value, asset.Id);
                    asset.IsBought = isBought != null;
                }
            }
            // check if user liked this artwork
            var isLiked = await _unitOfWork.LikeRepository.GetByIdAsync(accountId.Value, artworkId);
            artworkVM.IsLiked = isLiked != null;
        }

        // increase view count
        artwork.ViewCount++;
        _unitOfWork.ArtworkRepository.Update(artwork);
        await _unitOfWork.SaveChangesAsync();

        return artworkVM;
    }

    public async Task<ArtworkDetailModerationVM?> GetArtworkByIdForModerationAsync(Guid artworkId)
    {
        var artwork = await _unitOfWork.ArtworkRepository.GetArtworkDetailByIdAsync(artworkId)
            ?? throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        var artworkVM = _mapper.Map<ArtworkDetailModerationVM>(artwork);

        await _unitOfWork.SaveChangesAsync();

        return artworkVM;
    }

    public async Task<ArtworkVM> AddArtworkAsync(ArtworkModel artworkModel)
    {
        var currentUserId = _claimService.GetCurrentUserId ?? default;
        var currentUsername = _claimService.GetCurrentUserName ?? default;

        var newArtwork = _mapper.Map<Artwork>(artworkModel);
        string newThumbnailName = newArtwork.Id + "_t";
        string folderName = THUMBNAIL_ARTWORK_FOLDER;
        string extension = System.IO.Path.GetExtension(artworkModel.Thumbnail.FileName);
        
        // Upload thumbnail lên Cloudinary
        var url = await _cloudinaryService.UploadFileAsync(artworkModel.Thumbnail, newThumbnailName, folderName)
            ?? throw new KeyNotFoundException("Lỗi khi tải ảnh đại diện lên Cloudinary.");
        
        newArtwork.Thumbnail = url;
        newArtwork.ThumbnailName = newThumbnailName + extension;
        // them artwork 
        await _unitOfWork.ArtworkRepository.AddAsync(newArtwork);

        // them software used
        if (artworkModel.SoftwareUseds != null)
        {
            SoftwareListArtworkModel softwareListArtworkModel = new()
            {
                ArtworkId = newArtwork.Id,
                SoftwareList = artworkModel.SoftwareUseds
            };
            await _softwareDetailService.AddSoftwareListArtworkAsync(softwareListArtworkModel, false);
        }

        // them tag 
        TagListArtworkModel tagListArtworkModel = new()
        {
            ArtworkId = newArtwork.Id,
            TagList = artworkModel.Tags
        };
        await _tagDetailService.AddTagListArtworkAsync(tagListArtworkModel, false);

        // them cate
        CategoryListArtworkModel categoryList = new()
        {
            ArtworkId = newArtwork.Id,
            CategoryList = artworkModel.Categories
        };
        await _categoryArtworkDetailService.AddCategoryListArtworkAsync(categoryList, false);

        // them hinh anh
        MultiImageModel multiImageModel = new()
        {
            ArtworkId = newArtwork.Id,
            Images = artworkModel.ImageFiles
        };
        await _imageService.AddRangeImageAsync(multiImageModel, false);

        bool flagPrice = false;
        // them asset
        if (artworkModel.AssetFiles != null)
        {
            var uploadAssetsTask = new List<Task>();
            foreach (var singleAsset in artworkModel.AssetFiles.Select((file, index) => (file, index)))
            {
                if (flagPrice == false && singleAsset.file.Price > 0)
                {
                    flagPrice = true;
                }

                string newAssetName = $"{Path.GetFileNameWithoutExtension(singleAsset.file.File.FileName)}_{DateTime.Now.Ticks}";
                string assetFolderName = ASSET_FOLDER;
                string imageExtension = Path.GetExtension(singleAsset.file.File.FileName);

                // Upload asset lên Cloudinary
                uploadAssetsTask.Add(Task.Run(async () =>
                {
                    var url = await _cloudinaryService.UploadFileAsync(singleAsset.file.File, newAssetName, assetFolderName, false)
                        ?? throw new KeyNotFoundException("Lỗi khi tải tài nguyên lên Cloudinary.");
                    
                    Asset newAsset = new()
                    {
                        ArtworkId = newArtwork.Id,
                        Location = url,
                        AssetName = newAssetName + imageExtension,
                        AssetTitle = singleAsset.file.AssetTitle,
                        Description = singleAsset.file.Description,
                        Price = singleAsset.file.Price,
                        ContentType = imageExtension.Replace(".", ""),
                        Size = (ulong)singleAsset.file.File.Length
                    };
                    await _unitOfWork.AssetRepository.AddAsync(newAsset);
                }));
            }
            await Task.WhenAll(uploadAssetsTask);
        }

        if (flagPrice)
            newArtwork.State = StateEnum.Waiting;
        else
            newArtwork.State = StateEnum.Accepted;

        await _unitOfWork.SaveChangesAsync();

        var result = await _unitOfWork.ArtworkRepository.GetArtworkDetailByIdAsync(newArtwork.Id);
        var resultVM = _mapper.Map<ArtworkVM>(result);
        
        // Index to Elasticsearch if available
        _elasticClient?.IndexDocument(resultVM);

        if (newArtwork.State == StateEnum.Accepted && newArtwork.Privacy == PrivacyEnum.Public)
        {
            var listFollowers = await _unitOfWork.FollowRepository.GetAllFollowersAsync(currentUserId);
            var listNotification = new List<NotificationModel>();
            foreach (var follower in listFollowers)
            {
                var notification = new NotificationModel
                {
                    SentToAccount = follower.FollowingId,
                    Content = $"Người dùng [{currentUsername}] đăng tác phẩm [{newArtwork.Title}]",
                    NotifyType = NotifyTypeEnum.Information,
                    ReferencedAccountId = currentUserId,
                    ReferencedArtworkId = newArtwork.Id
                };
                listNotification.Add(notification);
            }
            await _notificationService.AddRangeNotificationAsync(listNotification);
        }

        return resultVM;
    }

    public async Task DeleteArtworkAsync(Guid artworkId)
    {
        // check if artwork exist
        var artwork = await _unitOfWork.ArtworkRepository.GetByIdAsync(artworkId);
        if (artwork == null)
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");

        // check authorized
        if (_claimService.GetCurrentRole.Equals(RoleEnum.CommonUser.ToString()) &&
            artwork.CreatedBy != _claimService.GetCurrentUserId)
        {
            throw new UnauthorizedAccessException("Bạn không có quyền xóa tác phẩm.");
        }

        _unitOfWork.ArtworkRepository.Delete(artwork);
        
        // Delete from Elasticsearch if available
        _elasticClient?.Delete<ArtworksV2>(artworkId);
        
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SoftDeleteArtworkAsync(Guid artworkId)
    {
        Console.WriteLine($"[SoftDeleteArtworkAsync] Attempting to delete artwork: {artworkId}");
        Console.WriteLine($"[SoftDeleteArtworkAsync] Current user: {_claimService.GetCurrentUserId}, Role: {_claimService.GetCurrentRole}");
        
        var artwork = await _unitOfWork.ArtworkRepository.GetByIdAsync(artworkId);
        if (artwork == null)
        {
            Console.WriteLine($"[SoftDeleteArtworkAsync] Artwork not found: {artworkId}");
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        }

        Console.WriteLine($"[SoftDeleteArtworkAsync] Found artwork: {artworkId}, CreatedBy: {artwork.CreatedBy}, DeletedOn: {artwork.DeletedOn}");

        // check authorized
        if (_claimService.GetCurrentRole.Equals(RoleEnum.CommonUser.ToString()) &&
            artwork.CreatedBy != _claimService.GetCurrentUserId)
        {
            Console.WriteLine($"[SoftDeleteArtworkAsync] Unauthorized: User {_claimService.GetCurrentUserId} cannot delete artwork created by {artwork.CreatedBy}");
            throw new UnauthorizedAccessException("Bạn không có quyền xóa tác phẩm.");
        }
        
        Console.WriteLine($"[SoftDeleteArtworkAsync] Soft deleting artwork: {artworkId}");
        _unitOfWork.ArtworkRepository.SoftDelete(artwork);
        
        // Update Elasticsearch if available
        _elasticClient?.Update<ArtworksV2, object>(artworkId, u => u.Doc(new { artwork.DeletedOn }));
        
        await _unitOfWork.SaveChangesAsync();
        Console.WriteLine($"[SoftDeleteArtworkAsync] Successfully soft deleted artwork: {artworkId}");
    }

    public async Task UpdateArtworkAsync(Guid artworkId, ArtworkEM artworkEM)
    {
        var oldArtwork = await _unitOfWork.ArtworkRepository.GetByIdAsync(artworkId);
        if (oldArtwork == null)
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");

        oldArtwork.Title = artworkEM.Title;
        oldArtwork.Description = artworkEM.Description;
        oldArtwork.Privacy = artworkEM.Privacy;
        // chua update AI, thumbnail, imagefiles, tags, categories
        _unitOfWork.ArtworkRepository.Update(oldArtwork);
        
        // Update Elasticsearch if available
        _elasticClient?.Update<ArtworksV2, object>(artworkId, u => u.Doc(new { oldArtwork.Title, oldArtwork.Description, oldArtwork.Privacy }));
        
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateArtworkStateAsync(Guid artworkId, ArtworkStateEM model)
    {
        var currentUserId = _claimService.GetCurrentUserId ?? default;
        var currentRole = _claimService.GetCurrentRole;

        var oldArtwork = await _unitOfWork.ArtworkRepository.GetArtworkDetailByIdAsync(artworkId)
            ?? throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        if (oldArtwork.State != StateEnum.Waiting)
            throw new BadHttpRequestException($"Tác phẩm đã được xử lý (trạng thái hiện tại là '{STATE_ENUM_VN[oldArtwork.State]}')");
        oldArtwork.State = model.State;
        oldArtwork.Note = model.Note;
        _unitOfWork.ArtworkRepository.Update(oldArtwork);
        
        // Update Elasticsearch if available
        _elasticClient?.Update<ArtworksV2, object>(artworkId, u => u.Doc(new { State = oldArtwork.State.ToString(), oldArtwork.Note }));
        
        await _unitOfWork.SaveChangesAsync();

        // add new notification
        if (model.State == StateEnum.Accepted)
        {
            var notification = new NotificationModel
            {
                SentToAccount = oldArtwork.CreatedBy!.Value,
                Content = $"Tác phẩm [{oldArtwork.Title}] đã được chấp nhận",
                NotifyType = NotifyTypeEnum.System,
                ReferencedAccountId = currentUserId
            };
            await _notificationService.AddNotificationAsync(notification, false);

            if (oldArtwork.Privacy == PrivacyEnum.Public)
            {
                var listFollowers = await _unitOfWork.FollowRepository.GetAllFollowersAsync(oldArtwork.CreatedBy!.Value);
                var listNotification = new List<NotificationModel>();
                foreach (var follower in listFollowers)
                {
                    var followNotification = new NotificationModel
                    {
                        SentToAccount = follower.FollowingId,
                        Content = $"Người dùng [{oldArtwork.Account.Username}] đăng tác phẩm [{oldArtwork.Title}]",
                        NotifyType = NotifyTypeEnum.Information,
                        ReferencedAccountId = oldArtwork.Account.Id,
                        ReferencedArtworkId = oldArtwork.Id
                    };
                    listNotification.Add(followNotification);
                }
                await _notificationService.AddRangeNotificationAsync(listNotification, false);
                await _unitOfWork.SaveChangesAsync();
            }

        }
        else
        {
            var notification = new NotificationModel
            {
                SentToAccount = oldArtwork.CreatedBy!.Value,
                Content = $"Tác phẩm [{oldArtwork.Title}] đã bị từ chối",
                NotifyType = NotifyTypeEnum.System,
                ReferencedAccountId = oldArtwork.Account.Id,
                ReferencedArtworkId = oldArtwork.Id
            };
            await _notificationService.AddNotificationAsync(notification);
        }
    }
}
