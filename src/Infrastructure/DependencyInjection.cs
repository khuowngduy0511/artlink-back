using Domain.Repositories.Abstractions;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Commons;
using Infrastructure.Services;
using Application.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        services.AddScoped<IArtworkRepository, ArtworkRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IBookmarkRepository, BookmarkRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryArtworkDetailRepository, CategoryArtworkDetailRepository>();
        services.AddScoped<ICategoryServiceDetailRepository, CategoryServiceDetailRepository>();
        services.AddScoped<IChatBoxRepository, ChatBoxRepository>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<ILicenseTypeRepository, LicenseTypeRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IProposalAssetRepository, ProposalAssetRepository>();
        services.AddScoped<IProposalRepository, ProposalRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IServiceDetailRepository, ServiceDetailRepository>();
        services.AddScoped<ISoftwareDetailRepository, SoftwareDetailRepository>();
        services.AddScoped<ISoftwareUsedRepository, SoftwareUsedRepository>();
        services.AddScoped<ITagDetailRepository, TagDetailRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
        services.AddScoped<IWalletHistoryRepository, WalletHistoryRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<IFollowRepository, FollowRepository>();
        services.AddScoped<IBlockRepository, BlockRepository>();
        services.AddScoped<IMilestoneRepository, MilestoneRepository>();
        services.AddScoped<IWithdrawalRequestRepository, WithdrawalRequestRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Add PayOS Service
        services.AddScoped<IPayOSService, PayOSService>();
        
        // Add Cloudinary Service
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        
        return services;
    }
}
