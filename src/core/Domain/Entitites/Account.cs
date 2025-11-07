using Domain.Attributes;
using Domain.Entities.Commons;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class Account : BaseEntity, ICreation, IModification, ISoftDelete
{
    [MaxLength(150)]
    public string Username { get; set; } = default!;
    [MaxLength(255)]
    public string? Password { get; set; } // remember to hash password before save/check
    [MaxLength(255)]
    public string Email { get; set; } = default!;
    [MaxLength(150)]
    public string Fullname { get; set; } = default!;
    [MaxLength(300)]
    public string? Bio { get; set; }
    public string? Avatar { get; set; }
    [Birthdate]
    public DateTime? Birthdate { get; set; }
    public string? Address { get; set; }
    public string? ArtisticStyle { get; set; }
    public DateTime? VerifiedOn { get; set; }

    public RoleEnum Role { get; set; } = RoleEnum.CommonUser;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    public virtual Wallet? Wallet { get; set; }
    public virtual ICollection<ChatBox> ChatBoxes_1 { get; set; } = new List<ChatBox>();
    public virtual ICollection<ChatBox> ChatBoxes_2 { get; set; } = new List<ChatBox>();
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    public virtual ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
    public virtual ICollection<Proposal> OrderProposals { get; set; } = new List<Proposal>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();
    public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
    public virtual ICollection<TransactionHistory> TransactionHistoriesReceivedCoins { get; set; } = new List<TransactionHistory>();
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
    public virtual ICollection<Follow> Followings { get; set; } = new List<Follow>(); // nguoi dang theo doi
    public virtual ICollection<Follow> Followers { get; set; } = new List<Follow>(); // nguoi dc theo doi
    public virtual ICollection<Block> Blocking { get; set; } = new List<Block>(); // nguoi block
    public virtual ICollection<Block> Blocked { get; set; } = new List<Block>(); // nguoi bi block
    public virtual ICollection<WalletHistory> WalletHistories { get; set; } = new List<WalletHistory>();
    public virtual ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();
    public virtual ICollection<ArtistCertificate> ArtistCertificates { get; set; } = new List<ArtistCertificate>();
}
