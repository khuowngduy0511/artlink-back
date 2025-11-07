using Application.Commons;
using Application.Services.Abstractions;
using Application.Services.Authentication;
using Application.Services.Firebase;
using AutoFixture;
using AutoMapper;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebApi;

namespace Domain.Test;

public class SetupTest : IDisposable
{
    protected readonly Fixture _fixture;
    protected readonly Mock<AppConfiguration> _appConfigMock;
    protected readonly Mock<IClaimService> _claimsServiceMock;
    protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
    protected readonly IMapper _mapperConfig;
    protected readonly AppDBContext _dbContext;

    #region repository mocks
    protected readonly Mock<IAccountRepository> _accountRepositoryMock;
    protected readonly Mock<IArtworkRepository> _artworkRepositoryMock;
    protected readonly Mock<IAssetRepository> _assetRepositoryMock;
    protected readonly Mock<IBlockRepository> _blockRepositoryMock;
    protected readonly Mock<IBookmarkRepository> _bookmarkRepositoryMock;
    protected readonly Mock<ICategoryArtworkDetailRepository> _categoryArtworkDetailRepositoryMock;
    protected readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    protected readonly Mock<ICategoryServiceDetailRepository> _categoryServiceDetailRepositoryMock;
    protected readonly Mock<IChatBoxRepository> _chatBoxRepositoryMock;
    protected readonly Mock<ICollectionRepository> _collectionRepositoryMock;
    protected readonly Mock<ICommentRepository> _commentRepositoryMock;
    protected readonly Mock<IFollowRepository> _followRepositoryMock;
    protected readonly Mock<IImageRepository> _imageRepositoryMock;
    protected readonly Mock<ILicenseTypeRepository> _licenseTypeRepositoryMock;
    protected readonly Mock<ILikeRepository> _likeRepositoryMock;
    protected readonly Mock<IMessageRepository> _messageRepositoryMock;
    protected readonly Mock<IMilestoneRepository> _milestoneRepositoryMock;
    protected readonly Mock<INotificationRepository> _notificationRepositoryMock;
    protected readonly Mock<IProposalAssetRepository> _proposalAsseRepositoryMock;
    protected readonly Mock<IProposalRepository> _proposalRepositoryMock;
    protected readonly Mock<IReportRepository> _reportRepositoryMock;
    protected readonly Mock<IRequestRepository> _requestRepositoryMock;
    protected readonly Mock<IReviewRepository> _reviewRepositoryMock;
    protected readonly Mock<IServiceDetailRepository> _serviceDetailRepositoryMock;
    protected readonly Mock<IServiceRepository> _serviceRepositoryMock;
    protected readonly Mock<ISoftwareDetailRepository> _softwareDetailRepositoryMock;
    protected readonly Mock<ISoftwareUsedRepository> _softwareUsedRepositoryMock;
    protected readonly Mock<ITagDetailRepository> _tagDetailRepositoryMock;
    protected readonly Mock<ITagRepository> _tagRepositoryMock;
    protected readonly Mock<ITransactionHistoryRepository> _transactionHistoryRepositoryMock;
    protected readonly Mock<IUserTokenRepository> _userTokenRepositoryMock;
    protected readonly Mock<IWalletHistoryRepository> _walletHistoryRepositoryMock;
    protected readonly Mock<IWalletRepository> _walletRepositoryMock;
    #endregion

    #region service mocks
    protected readonly Mock<IAccountService> _accountServiceMock;
    protected readonly Mock<IArtworkService> _artworkServiceMock;
    protected readonly Mock<IAssetService> _assetServiceMock;
    protected readonly Mock<IAssetTransactionService> _assetTransactionServiceMock;
    protected readonly Mock<IBlockService> _blockServiceMock;
    protected readonly Mock<ICategoryArtworkDetailService> _categoryArtworkDetailServiceMock;
    protected readonly Mock<ICategoryService> _categoryServiceMock;
    protected readonly Mock<ICategoryServiceDetailService> _categoryServiceDetailServiceMock;
    protected readonly Mock<IChatBoxService> _chatBoxServiceMock;
    protected readonly Mock<ICollectionService> _collectionServiceMock;
    protected readonly Mock<ICommentService> _commentServiceMock;
    protected readonly Mock<IFollowService> _followServiceMock;
    protected readonly Mock<IImageService> _imageServiceMock;
    protected readonly Mock<ILicenseTypeService> _licenseTypeServiceMock;
    protected readonly Mock<ILikeService> _likeServiceMock;
    protected readonly Mock<IMessageService> _messageServiceMock;
    protected readonly Mock<INotificationService> _notificationServiceMock;
    protected readonly Mock<IProposalAssetService> _proposalAssetServiceMock;
    protected readonly Mock<IProposalService> _proposalServiceMock;
    protected readonly Mock<IReportService> _reportServiceMock;
    protected readonly Mock<IRequestService> _requestServiceMock;
    protected readonly Mock<IReviewService> _reviewServiceMock;
    protected readonly Mock<IServiceService> _serviceServiceMock;
    protected readonly Mock<ISoftwareDetailService> _softwareDetailServiceMock;
    protected readonly Mock<ISoftwareUsedService> _softwareUsedServiceMock;
    protected readonly Mock<ITagDetailService> _tagDetailServiceMock;
    protected readonly Mock<ITagService> _tagServiceMock;
    protected readonly Mock<ITransactionHistoryService> _transactionHistoryServiceMock;
    protected readonly Mock<IUserTokenService> _userTokenServiceMock;
    protected readonly Mock<IWalletHistoryService> _walletHistoryServiceMock;
    protected readonly Mock<IWalletService> _walletServiceMock;
    protected readonly Mock<IFirebaseService> _firebaseServiceMock;

    protected readonly Mock<ITokenHandler> _tokenHandlerMock;
    protected readonly Mock<IThirdAuthenticationService> _thirdAuthenticationServiceMock;
    #endregion

    public SetupTest()
    {
        _appConfigMock = new Mock<AppConfiguration>();
        _claimsServiceMock = new Mock<IClaimService>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        // set up fixture
        _fixture = new Fixture();
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        // set up auto mapper
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        _mapperConfig = mappingConfig.CreateMapper();

        // set up db context
        var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        _dbContext = new AppDBContext(options, _appConfigMock.Object);

        #region setup repositories mock

        _accountRepositoryMock = new Mock<IAccountRepository>();
        _artworkRepositoryMock = new Mock<IArtworkRepository>();
        _assetRepositoryMock = new Mock<IAssetRepository>();
        _blockRepositoryMock = new Mock<IBlockRepository>();
        _bookmarkRepositoryMock = new Mock<IBookmarkRepository>();
        _categoryArtworkDetailRepositoryMock = new Mock<ICategoryArtworkDetailRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _categoryServiceDetailRepositoryMock = new Mock<ICategoryServiceDetailRepository>();
        _chatBoxRepositoryMock = new Mock<IChatBoxRepository>();
        _collectionRepositoryMock = new Mock<ICollectionRepository>();
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _followRepositoryMock = new Mock<IFollowRepository>();
        _imageRepositoryMock = new Mock<IImageRepository>();
        _licenseTypeRepositoryMock = new Mock<ILicenseTypeRepository>();
        _likeRepositoryMock = new Mock<ILikeRepository>();
        _messageRepositoryMock = new Mock<IMessageRepository>();
        _notificationRepositoryMock = new Mock<INotificationRepository>();
        _proposalAsseRepositoryMock = new Mock<IProposalAssetRepository>();
        _proposalRepositoryMock = new Mock<IProposalRepository>();
        _reportRepositoryMock = new Mock<IReportRepository>();
        _requestRepositoryMock = new Mock<IRequestRepository>();
        _reviewRepositoryMock = new Mock<IReviewRepository>();
        _serviceDetailRepositoryMock = new Mock<IServiceDetailRepository>();
        _serviceRepositoryMock = new Mock<IServiceRepository>();
        _softwareDetailRepositoryMock = new Mock<ISoftwareDetailRepository>();
        _softwareUsedRepositoryMock = new Mock<ISoftwareUsedRepository>();
        _tagDetailRepositoryMock = new Mock<ITagDetailRepository>();
        _tagRepositoryMock = new Mock<ITagRepository>();
        _transactionHistoryRepositoryMock = new Mock<ITransactionHistoryRepository>();
        _userTokenRepositoryMock = new Mock<IUserTokenRepository>();
        _walletHistoryRepositoryMock = new Mock<IWalletHistoryRepository>();
        _walletRepositoryMock = new Mock<IWalletRepository>();
        _milestoneRepositoryMock = new Mock<IMilestoneRepository>();

        #endregion

        #region setup services mock

        _accountServiceMock = new Mock<IAccountService>();
        _artworkServiceMock = new Mock<IArtworkService>();
        _assetServiceMock = new Mock<IAssetService>();
        _assetTransactionServiceMock = new Mock<IAssetTransactionService>();
        _blockServiceMock = new Mock<IBlockService>();
        _categoryArtworkDetailServiceMock = new Mock<ICategoryArtworkDetailService>();
        _categoryServiceMock = new Mock<ICategoryService>();
        _categoryServiceDetailServiceMock = new Mock<ICategoryServiceDetailService>();
        _chatBoxServiceMock = new Mock<IChatBoxService>();
        _collectionServiceMock = new Mock<ICollectionService>();
        _commentServiceMock = new Mock<ICommentService>();
        _followServiceMock = new Mock<IFollowService>();
        _imageServiceMock = new Mock<IImageService>();
        _licenseTypeServiceMock = new Mock<ILicenseTypeService>();
        _likeServiceMock = new Mock<ILikeService>();
        _messageServiceMock = new Mock<IMessageService>();
        _milestoneRepositoryMock = new Mock<IMilestoneRepository>();
        _notificationServiceMock = new Mock<INotificationService>();
        _proposalAssetServiceMock = new Mock<IProposalAssetService>();
        _proposalServiceMock = new Mock<IProposalService>();
        _reportServiceMock = new Mock<IReportService>();
        _requestServiceMock = new Mock<IRequestService>();
        _reviewServiceMock = new Mock<IReviewService>();
        _serviceServiceMock = new Mock<IServiceService>();
        _softwareDetailServiceMock = new Mock<ISoftwareDetailService>();
        _softwareUsedServiceMock = new Mock<ISoftwareUsedService>();
        _tagDetailServiceMock = new Mock<ITagDetailService>();
        _tagServiceMock = new Mock<ITagService>();
        _transactionHistoryServiceMock = new Mock<ITransactionHistoryService>();
        _userTokenServiceMock = new Mock<IUserTokenService>();
        _walletHistoryServiceMock = new Mock<IWalletHistoryService>();
        _walletServiceMock = new Mock<IWalletService>();
        _tokenHandlerMock = new Mock<ITokenHandler>();
        _thirdAuthenticationServiceMock = new Mock<IThirdAuthenticationService>();
        _firebaseServiceMock = new Mock<IFirebaseService>();
        #endregion
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    // mock data methods
    protected List<Account> MockAccountList(int length)
    {
        return _fixture.Build<Account>()
                       .Without(x => x.Wallet)
                       .Without(x => x.ChatBoxes_1)
                       .Without(x => x.ChatBoxes_2)
                       .Without(x => x.Messages)
                       .Without(x => x.Proposals)
                       .Without(x => x.Reviews)
                       .Without(x => x.Services)
                       .Without(x => x.Requests)
                       .Without(x => x.UserTokens)
                       .Without(x => x.Artworks)
                       .Without(x => x.Notifications)
                       .Without(x => x.TransactionHistories)
                       .Without(x => x.Likes)
                       .Without(x => x.Comments)
                       .Without(x => x.Collections)
                       .Without(x => x.Reports)
                       .Without(x => x.Followings)
                       .Without(x => x.Followers)
                       .Without(x => x.Blocking)
                       .Without(x => x.Blocked)
                       .CreateMany(length)
                       .ToList();
    }

    protected List<Artwork> MockArtworkList(int length)
    {
        return _fixture.Build<Artwork>()
                       .CreateMany(length)
                       .ToList();
    }
    protected List<Asset> MockAssetList(int length)
    {
        return _fixture.Build<Asset>()
                       .Without(x => x.Artwork)
                       .Without(x => x.TransactionHistories)
                       .CreateMany(length)
                       .ToList();
    }
    protected List<Comment> MockCommentList(int length)
    {
        return _fixture.Build<Comment>()
                       .Without(x => x.Artwork)
                       .Without(x => x.Reply)
                       .Without(x => x.Account)
                       .Without(x => x.Replies)
                       .CreateMany(length)
                       .ToList();
    }
    protected List<Follow> MockFollowList(int length)
    {
        return _fixture.Build<Follow>()
                       .Without(x => x.Followed)
                       .Without(x => x.Following)
                       .CreateMany(length)
                       .ToList();
    }

    protected List<Like> MockLikeList(int length)
    {
        return _fixture.Build<Like>()
                       .Without(x => x.Account)
                       .Without(x => x.Artwork)
                       .CreateMany(length)
                       .ToList();
    }

    protected List<Tag> MockTagList(int length)
    {
        return _fixture.Build<Tag>()
                    .Without(x => x.TagDetails)
                    .CreateMany(length)
                    .ToList();
    }

    protected List<Category> MockCategoryList(int length)
    {
        return _fixture.Build<Category>()
                       .Without(x => x.CategoryArtworkDetails)
                       .Without(x => x.CategoryServiceDetails)
                       .CreateMany(length)
                       .ToList();
    }
}
