using Domain.Enums;

namespace Application.Commons;

public static class VietnameseEnum
{
    public static readonly Dictionary<NotifyTypeEnum, string> NOTIFYTYPE_ENUM_VN = new()
    {
        { NotifyTypeEnum.System, "Hệ thống" },
        { NotifyTypeEnum.Warning, "Cảnh báo" },
        { NotifyTypeEnum.Information, "Thông tin" }
    };

    public static readonly Dictionary<ProposalAssetEnum, string> PROPOSALASSET_ENUM_VN = new()
    {
        { ProposalAssetEnum.Concept, "Phác thảo" },
        { ProposalAssetEnum.Final, "Cuối cùng" },
        { ProposalAssetEnum.Revision, "Sửa đổi" }
    };

    public static readonly Dictionary<ReportEntityEnum, string> REPORTENTITY_ENUM_VN = new()
    {
        { ReportEntityEnum.Comment, "Bình luận" },
        { ReportEntityEnum.Account, "Tài khoản" },
        { ReportEntityEnum.Artwork, "Tác phẩm" }
    };

    public static readonly Dictionary<PrivacyEnum, string> PRIVACY_ENUM_VN = new()
    {
        { PrivacyEnum.Public, "Công khai" },
        { PrivacyEnum.Private, "Riêng tư" }
    };

    public static readonly Dictionary<ProposalStateEnum, string> PROPOSALSTATE_ENUM_VN = new()
    {
        { ProposalStateEnum.Waiting, "Đang chờ" },
        { ProposalStateEnum.Accepted, "Chấp nhận" },
        { ProposalStateEnum.Declined, "Từ chối" },
        { ProposalStateEnum.Cancelled, "Hủy bỏ" },
        { ProposalStateEnum.InitPayment, "Đã đặt cọc" },
        { ProposalStateEnum.Completed, "Kết thúc" },
        { ProposalStateEnum.CompletePayment, "Thanh toán toàn bộ" },
        { ProposalStateEnum.ConfirmPayment, "Xác nhận thanh toán" }
    };

    public static readonly Dictionary<ReportTypeEnum, string> REPORTTYPE_ENUM_VN = new()
    {
        { ReportTypeEnum.Harassment, "Quấy rối" },
        { ReportTypeEnum.HateSpeech, "Phát ngôn thù hận" },
        { ReportTypeEnum.Spam, "Spam" },
        { ReportTypeEnum.Impersonation, "Giả mạo" },
        { ReportTypeEnum.InappropriateContent, "Nội dung không phù hợp" },
        { ReportTypeEnum.Other, "Khác" }
    };

    public static readonly Dictionary<RoleEnum, string> ROLETYPE_ENUM_VN = new()
    {
        { RoleEnum.CommonUser, "Người dùng" },
        { RoleEnum.Admin, "Quản trị viên" },
        { RoleEnum.Moderator, "Kiểm soát viên" },
    };

    public static readonly Dictionary<StateEnum, string> STATE_ENUM_VN = new()
    {
        { StateEnum.Waiting, "Đang chờ" },
        { StateEnum.Accepted, "Chấp nhận" },
        { StateEnum.Declined, "Từ chối" },
        { StateEnum.Cancelled, "Hủy bỏ" }
    };

    public static readonly Dictionary<TransactionStatusEnum, string> TRANSACTIONSTATUS_ENUM_VN = new()
    {
        { TransactionStatusEnum.InProgress, "Đang xử lý" },
        { TransactionStatusEnum.Success, "Thành công" },
        { TransactionStatusEnum.Failed, "Thất bại" },
        { TransactionStatusEnum.Cancel, "Hủy bỏ" }
    };

    public static readonly Dictionary<WalletHistoryTypeEnum, string> WALLETHISTORYTYPE_ENUM_VN = new()
    {
        { WalletHistoryTypeEnum.Deposit, "Nạp tiền" },
        { WalletHistoryTypeEnum.Withdraw, "Rút tiền" }
    };
}