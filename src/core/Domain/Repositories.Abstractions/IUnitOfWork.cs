namespace Domain.Repositories.Abstractions;

public interface IUnitOfWork
{
    public IAccountRepository AccountRepository { get; }
    public IArtworkRepository ArtworkRepository { get; }
    public IAssetRepository AssetRepository { get; }
    public IBookmarkRepository BookmarkRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ICategoryArtworkDetailRepository CategoryArtworkDetailRepository { get; }
    public ICategoryServiceDetailRepository CategoryServiceDetailRepository { get; }
    public IChatBoxRepository ChatBoxRepository { get; }
    public ICollectionRepository CollectionRepository { get; }
    public ICommentRepository CommentRepository { get; }
    public ILikeRepository LikeRepository { get; }
    public ILicenseTypeRepository LicenseTypeRepository { get; }
    public IMessageRepository MessageRepository { get; }
    public IFollowRepository FollowRepository { get; }
    public INotificationRepository NotificationRepository { get; }
    public IProposalAssetRepository ProposalAssetRepository { get; }
    public IProposalRepository ProposalRepository { get; }
    public IReportRepository ReportRepository { get; }
    public IRequestRepository RequestRepository { get; }
    public IReviewRepository ReviewRepository { get; }
    public IServiceRepository ServiceRepository { get; }
    public IServiceDetailRepository ServiceDetailRepository { get; }
    public ISoftwareDetailRepository SoftwareDetailRepository { get; }
    public ISoftwareUsedRepository SoftwareUsedRepository { get; }
    public ITagDetailRepository TagDetailRepository { get; }
    public ITagRepository TagRepository { get; }
    public ITransactionHistoryRepository TransactionHistoryRepository { get; }
    public IWalletHistoryRepository WalletHistoryRepository { get; }
    public IWalletRepository WalletRepository { get; }
    public IUserTokenRepository UserTokenRepository { get; }
    public IImageRepository ImageRepository { get; }
    public IBlockRepository BlockRepository { get; }
    public IMilestoneRepository MilestoneRepository { get; }
    public IWithdrawalRequestRepository WithdrawalRequestRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
