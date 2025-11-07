using Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class Asset : BaseEntity, IModification, ISoftDelete
{
    public Guid ArtworkId { get; set; }
    [MaxLength(150)]
    public string AssetTitle { get; set; } = default!; // ten asset (vd: Bia sach 2023)
    [MaxLength(500)]
    public string? Description { get; set; } // mo ta asset
    [MaxLength(150)]
    public string AssetName { get; set; } = default!; // ten file asset do he thong dat ten (vd: biasach2023.zip)
    public string Location { get; set; } = default!; // url address
    public double Price { get; set; } = 0;
    public string ContentType { get; set; } = default!; // loai duoi file (rar, zip)
    public ulong Size { get; set; } = default!; // dung luong asset, don vi byte
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }

    public virtual Artwork Artwork { get; set; } = default!;
    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
}
