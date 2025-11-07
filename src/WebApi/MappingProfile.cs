using Application.Commons;
using Application.Models;
using Application.Models.ZaloPayModels;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Domain.Models;
namespace WebApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap(typeof(PagedList<>), typeof(PagedList<>));

        #region Account
        CreateMap<Account, AccountVM>();

        CreateMap<Account, AccountModerationVM>();

        CreateMap<Account, AccountBasicInfoVM>();

        CreateMap<Account, HiredAccountVM>()
            .ForMember(model => model.IsVerified, opt => opt.MapFrom(src => src.VerifiedOn != null))
            .ForMember(model => model.ProjectCompleted,
                opt => opt.MapFrom(x => x.Proposals
                .Where(p => p.ProposalStatus == ProposalStateEnum.Completed
                    || p.ProposalStatus == ProposalStateEnum.CompletePayment)
                .Count()));

        CreateMap<Account, RegisterModel>().ReverseMap();
        CreateMap<Account, AccountModel>().ReverseMap();
        CreateMap<Account, AccountDisplayModel>().ReverseMap();
        CreateMap<Account, AccountCreateModel>().ReverseMap();
        #endregion

        #region Artwork
        CreateMap<Artwork, ArtworkVM>()
            .ForMember(model => model.CategoryList, opt => opt.MapFrom(x => x.CategoryArtworkDetails.Select(y => y.Category).ToList()))
            .ForMember(model => model.TagList, opt => opt.MapFrom(x => x.TagDetails.Select(y => y.Tag).ToList()))
            .ForMember(model => model.SoftwareUseds, opt => opt.MapFrom(x => x.SoftwareDetails.Select(y => y.SoftwareUsed).ToList()));

        CreateMap<Artwork, ArtworkModerationVM>()
            .ForMember(model => model.CategoryList, opt => opt.MapFrom(x => x.CategoryArtworkDetails.Select(y => y.Category).ToList()))
            .ForMember(model => model.TagList, opt => opt.MapFrom(x => x.TagDetails.Select(y => y.Tag).ToList()))
            .ForMember(model => model.SoftwareUseds, opt => opt.MapFrom(x => x.SoftwareDetails.Select(y => y.SoftwareUsed).ToList()));

        CreateMap<Artwork, ArtworkDetailModerationVM>();
        CreateMap<Artwork, ArtworkPreviewVM>();
        CreateMap<Artwork, ArtworkContainAssetVM>();

        CreateMap<Artwork, ArtworkDisplayModel>()
            .ForMember(model => model.Author, opt => opt.MapFrom(src => src.Account));

        CreateMap<Artwork, ArtworkModel>().ReverseMap();
        #endregion

        #region Asset
        CreateMap<Asset, AssetModel>().ReverseMap();
        CreateMap<Asset, AssetVM>().ReverseMap();
        #endregion

        #region Block
        CreateMap<Block, BlockModel>().ReverseMap();
        CreateMap<Block, BlockVM>();
        #endregion

        #region Bookmark
        CreateMap<Bookmark, BookmarkVM>().ReverseMap();
        CreateMap<Bookmark, BookmarkModel>().ReverseMap();
        #endregion

        #region Category
        CreateMap<Category, CategoryModel>().ReverseMap();
        CreateMap<Category, CategoryVM>().ReverseMap();
        CreateMap<Category, CategoryEM>().ReverseMap();
        #endregion

        #region Collection
        CreateMap<Collection, CollectionDetailVM>()
            .ForMember(model => model.CreatedBy, opt => opt.MapFrom(src => src.Account))
            .ForMember(model => model.Artworks, opt => opt.MapFrom(src => src.Bookmarks));
        CreateMap<Collection, CollectionVM>()
            .ForMember(model => model.CreatedBy, opt => opt.MapFrom(src => src.Account))
            .ForMember(model => model.Items, opt => opt.MapFrom(src => src.Bookmarks.Count()))
            .ForMember(model => model.Thumbnail, opt => opt.MapFrom(src => src.Bookmarks.FirstOrDefault()!.Artwork.Thumbnail));
        CreateMap<Collection, CollectionModificationModel>().ReverseMap();
        CreateMap<CollectionCreationModel, Collection>().ReverseMap();
        #endregion

        #region Comment
        CreateMap<Comment, CommentVM>()
            .ForMember(model => model.CreatedBy, opt => opt.MapFrom(x => x.Account))
            .ForMember(model => model.ReplyCount, opt => opt.MapFrom(x => x.Replies.Count()));
        #endregion

        #region Follow
        CreateMap<Follow, FollowModel>().ReverseMap();
        CreateMap<Follow, FollowingVM>().ReverseMap();
        CreateMap<Follow, FollowerVM>().ReverseMap();
        #endregion

        #region Image
        CreateMap<Image, ImageModel>().ReverseMap();
        CreateMap<Image, ImageVM>().ReverseMap();
        CreateMap<Image, ImageDuplicationVM>().ReverseMap();
        #endregion

        #region Proposal
        CreateMap<Proposal, ProposalModel>().ReverseMap();
        CreateMap<Proposal, ProposalVM>()
            .ForMember(model => model.IsReviewed, opt => opt.MapFrom(src => src.Review != null))
            .ForMember(model => model.Creator, opt => opt.MapFrom(src => src.Account))
            .ForMember(model => model.Orderer, opt => opt.MapFrom(src => src.Orderer));
        #endregion

        #region Service
        CreateMap<Service, ServiceModel>().ReverseMap();
        CreateMap<Service, ServiceVM>()
            .ForMember(model => model.Categories, opt => opt.MapFrom(x => x.CategoryServiceDetails.Select(y => y.Category).ToList()))
            .ForMember(model => model.ArtworkReferences, opt => opt.MapFrom(x => x.ServiceDetails.Select(y => y.Artwork).ToList()));
        CreateMap<Service, ServiceBasicInfoVM>();
        #endregion

        #region Tag
        CreateMap<Tag, TagModel>().ReverseMap();
        CreateMap<Tag, TagVM>().ReverseMap();
        CreateMap<TagDetail, TagDetailVM>().ReverseMap();

        CreateMap<Tag, TagModel>().ReverseMap();
        CreateMap<Tag, TagVM>().ReverseMap();
        #endregion 

        #region TransactionHistory
        CreateMap<TransactionHistory, AssetTransactionModel>().ReverseMap();
        CreateMap<TransactionHistory, AssetTransactionVM>().ReverseMap();

        CreateMap<TransactionHistory, TransactionHistoryVM>()
            .ForMember(model => model.FromAccount, opt => opt.MapFrom(src => src.Account));
        CreateMap<TransactionHistory, TransactionHistoryForUserVM>()
            .ForMember(model => model.CreatedAccount, opt => opt.MapFrom(src => src.Account))
            .ForMember(model => model.OtherAccount, opt => opt.MapFrom(src => src.ToAccount));
        CreateMap<TransactionHistory, TransactionModel>().ReverseMap();
        #endregion

        #region Wallet
        CreateMap<Wallet, WalletModel>().ReverseMap();
        CreateMap<Wallet, WalletVM>().ReverseMap();
        CreateMap<Wallet, WalletEM>().ReverseMap();
        #endregion

        CreateMap<CategoryArtworkDetail, CategoryArtworkModel>().ReverseMap();
        CreateMap<CategoryArtworkDetail, CategoryArtworkVM>().ReverseMap();

        CreateMap<CategoryServiceDetail, CategoryServiceModel>().ReverseMap();
        CreateMap<CategoryServiceDetail, CategoryServiceVM>().ReverseMap();

        CreateMap<Like, LikeModel>().ReverseMap();
        CreateMap<Like, LikeVM>().ReverseMap();

        CreateMap<LicenseType, LicenseTypeModel>().ReverseMap();
        CreateMap<LicenseType, LicenseTypeVM>().ReverseMap();

        CreateMap<SoftwareUsed, SoftwareUsedModel>().ReverseMap();
        CreateMap<SoftwareUsed, SoftwareUsedVM>().ReverseMap();


        CreateMap<Request, RequestModel>().ReverseMap();
        CreateMap<Request, RequestVM>();

        CreateMap<Report, ReportModel>().ReverseMap();
        CreateMap<Report, ReportVM>().ReverseMap();


        CreateMap<WalletHistory, WalletHistoryVM>();


        CreateMap<ZPCreateOrderRequest, OrderCreateModel>().ReverseMap();

        CreateMap<ProposalAsset, ProposalAssetModel>().ReverseMap();
        CreateMap<ProposalAsset, ProposalAssetVM>();

        CreateMap<Milestone, MilestoneVM>().ReverseMap();

        CreateMap<Message, MessageModel>().ReverseMap();
        CreateMap<Message, MessageVM>().ReverseMap();

        CreateMap<ChatBox, ChatBoxVM>().ReverseMap();

        CreateMap<Review, ReviewModel>().ReverseMap();
        CreateMap<Review, ReviewVM>().ReverseMap();

        CreateMap<Notification, NotificationModel>().ReverseMap();
        CreateMap<Notification, NotificationVM>().ReverseMap();

        CreateMap<TopCreatorOfAssetTrans, TopCreatorOfAssetTransVM>();
        CreateMap<TopCreatorOfProposal, TopCreatorOfProposalVM>();
        CreateMap<TopServiceOfCreator, TopServiceOfCreatorVM>();
    }
}
