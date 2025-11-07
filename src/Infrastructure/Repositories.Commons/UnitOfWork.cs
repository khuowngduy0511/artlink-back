using Domain.Repositories.Abstractions;
using Infrastructure.Database;

namespace Infrastructure.Repositories.Commons;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDBContext _dbContext;
    private readonly IAccountRepository _accountRepository;
    private readonly IArtworkRepository _artworkRepository;
    private readonly IAssetRepository _assetRepository;
    private readonly IBlockRepository _blockRepository;
    private readonly IBookmarkRepository _bookmarkRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryArtworkDetailRepository _categoryArtworkDetailRepository;
    private readonly ICategoryServiceDetailRepository _categoryServiceDetailRepository;
    private readonly IChatBoxRepository _chatBoxRepository;
    private readonly ICollectionRepository _collectionRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IFollowRepository _followRepository;
    private readonly IImageRepository _imageRepository;
    private readonly ILicenseTypeRepository _licenseTypeRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IMilestoneRepository _milestoneRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IProposalAssetRepository _proposalAssetRepository;
    private readonly IProposalRepository _proposalRepository;
    private readonly IReportRepository _reportRepository;
    private readonly IRequestRepository _requestRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IServiceDetailRepository _serviceDetailRepository;
    private readonly ISoftwareDetailRepository _softwareDetailRepository;
    private readonly ISoftwareUsedRepository _softwareUsedRepository;
    private readonly ITagDetailRepository _tagDetailRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ITransactionHistoryRepository _transactionHistoryRepository;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IWalletHistoryRepository _walletHistoryRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IWithdrawalRequestRepository _withdrawalRequestRepository;

    public UnitOfWork(
        AppDBContext dbContext,
        IAccountRepository accountRepository,
        IArtworkRepository artworkRepository,
        IAssetRepository assetRepository,
        IBlockRepository blockRepository,
        IBookmarkRepository bookmarkRepository,
        ICategoryArtworkDetailRepository categoryArtworkDetailRepository,
        ICategoryRepository categoryRepository,
        ICategoryServiceDetailRepository categoryServiceDetailRepository,
        IChatBoxRepository chatBoxRepository,
        ICollectionRepository collectionRepository,
        ICommentRepository commentRepository,
        IFollowRepository followRepository,
        IImageRepository imageRepository,
        ILicenseTypeRepository licenseTypeRepository,
        ILikeRepository likeRepository,
        IMessageRepository messageRepository,
        IMilestoneRepository milestoneRepository,
        INotificationRepository notificationRepository,
        IProposalAssetRepository proposalAssetRepository,
        IProposalRepository proposalRepository,
        IReportRepository reportRepository,
        IRequestRepository requestRepository,
        IReviewRepository reviewRepository,
        IServiceDetailRepository serviceDetailRepository,
        IServiceRepository serviceRepository,
        ISoftwareDetailRepository softwareDetailRepository,
        ISoftwareUsedRepository softwareUsedRepository,
        ITagDetailRepository tagDetailRepository,
        ITagRepository tagRepository,
        ITransactionHistoryRepository transactionHistoryRepository,
        IUserTokenRepository userTokenRepository,
        IWalletHistoryRepository walletHistoryRepository,
        IWalletRepository walletRepository,
        IWithdrawalRequestRepository withdrawalRequestRepository)
    {
        _dbContext = dbContext;
        _accountRepository = accountRepository;
        _artworkRepository = artworkRepository;
        _assetRepository = assetRepository;
        _blockRepository = blockRepository;
        _bookmarkRepository = bookmarkRepository;
        _categoryArtworkDetailRepository = categoryArtworkDetailRepository;
        _categoryRepository = categoryRepository;
        _categoryServiceDetailRepository = categoryServiceDetailRepository;
        _chatBoxRepository = chatBoxRepository;
        _collectionRepository = collectionRepository;
        _commentRepository = commentRepository;
        _followRepository = followRepository;
        _imageRepository = imageRepository;
        _licenseTypeRepository = licenseTypeRepository;
        _likeRepository = likeRepository;
        _messageRepository = messageRepository;
        _milestoneRepository = milestoneRepository;
        _notificationRepository = notificationRepository;
        _proposalAssetRepository = proposalAssetRepository;
        _proposalRepository = proposalRepository;
        _reportRepository = reportRepository;
        _requestRepository = requestRepository;
        _reviewRepository = reviewRepository;
        _serviceDetailRepository = serviceDetailRepository;
        _serviceRepository = serviceRepository;
        _softwareDetailRepository = softwareDetailRepository;
        _softwareUsedRepository = softwareUsedRepository;
        _tagDetailRepository = tagDetailRepository;
        _tagRepository = tagRepository;
        _transactionHistoryRepository = transactionHistoryRepository;
        _userTokenRepository = userTokenRepository;
        _walletHistoryRepository = walletHistoryRepository;
        _walletRepository = walletRepository;
        _withdrawalRequestRepository = withdrawalRequestRepository;
    }

    public IAccountRepository AccountRepository => _accountRepository;
    public IArtworkRepository ArtworkRepository => _artworkRepository;
    public IAssetRepository AssetRepository => _assetRepository;
    public IBlockRepository BlockRepository => _blockRepository;
    public IBookmarkRepository BookmarkRepository => _bookmarkRepository;
    public ICategoryArtworkDetailRepository CategoryArtworkDetailRepository => _categoryArtworkDetailRepository;
    public ICategoryRepository CategoryRepository => _categoryRepository;
    public ICategoryServiceDetailRepository CategoryServiceDetailRepository => _categoryServiceDetailRepository;
    public IChatBoxRepository ChatBoxRepository => _chatBoxRepository;
    public ICollectionRepository CollectionRepository => _collectionRepository;
    public ICommentRepository CommentRepository => _commentRepository;
    public IFollowRepository FollowRepository => _followRepository;
    public IImageRepository ImageRepository => _imageRepository;
    public ILicenseTypeRepository LicenseTypeRepository => _licenseTypeRepository;
    public ILikeRepository LikeRepository => _likeRepository;
    public IMessageRepository MessageRepository => _messageRepository;
    public IMilestoneRepository MilestoneRepository => _milestoneRepository;
    public INotificationRepository NotificationRepository => _notificationRepository;
    public IProposalAssetRepository ProposalAssetRepository => _proposalAssetRepository;
    public IProposalRepository ProposalRepository => _proposalRepository;
    public IReportRepository ReportRepository => _reportRepository;
    public IRequestRepository RequestRepository => _requestRepository;
    public IReviewRepository ReviewRepository => _reviewRepository;
    public ISoftwareDetailRepository SoftwareDetailRepository => _softwareDetailRepository;
    public IServiceRepository ServiceRepository => _serviceRepository;
    public IServiceDetailRepository ServiceDetailRepository => _serviceDetailRepository;
    public ISoftwareUsedRepository SoftwareUsedRepository => _softwareUsedRepository;
    public ITagDetailRepository TagDetailRepository => _tagDetailRepository;
    public ITagRepository TagRepository => _tagRepository;
    public ITransactionHistoryRepository TransactionHistoryRepository => _transactionHistoryRepository;
    public IUserTokenRepository UserTokenRepository => _userTokenRepository;
    public IWalletHistoryRepository WalletHistoryRepository => _walletHistoryRepository;
    public IWalletRepository WalletRepository => _walletRepository;
    public IWithdrawalRequestRepository WithdrawalRequestRepository => _withdrawalRequestRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);
}
