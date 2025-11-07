using Application.Services;
using Application.Services.Abstractions;
using Application.Services.Authentication;
using Application.Services.Firebase;
using Application.Services.GoogleStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IArtworkService, ArtworkService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IBlockService, BlockService>();
        services.AddScoped<ICategoryArtworkDetailService, CategoryArtworkDetailService>();
        services.AddScoped<ICategoryServiceDetailService, CategoryServiceDetailService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICollectionService, CollectionService>();
        services.AddScoped<IFollowService, FollowService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<ILicenseTypeService, LicenseTypeService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IRequestService, RequestService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<ISoftwareUsedService, SoftwareUsedService>();
        services.AddScoped<ISoftwareDetailService, SoftwareDetailService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ITagDetailService, TagDetailService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IAssetTransactionService, AssetTransactionService>();
        services.AddScoped<IWalletHistoryService, WalletHistoryService>();
        services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();
        services.AddScoped<IProposalService, ProposalService>();
        services.AddScoped<IProposalAssetService, ProposalAssetService>();
        services.AddScoped<IMilestoneService, MilestoneService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IChatBoxService, ChatBoxService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IDashBoardService, DashBoardService>();

        services.AddScoped<IUserTokenService, UserTokenService>();
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IFirebaseService, FirebaseService>();
        services.AddScoped<ICloudStorageService, CloudStorageService>();
        services.AddScoped<IThirdAuthenticationService, ThirdAuthenticationService>();
        services.AddScoped<IZaloPayService, ZaloPayService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOtpService, OtpService>();
        return services;
    }
}
