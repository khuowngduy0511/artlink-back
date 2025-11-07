using Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entitites;
public class Image : BaseEntity, IModification
{
    public Guid ArtworkId { get; set; }
    [MaxLength(150)]
    public string ImageName { get; set; } = default!;
    public string Location { get; set; } = default!; // url address
    public int Order { get; set; }
    public ulong? ImageHash { get; set; }
    [NotMapped]
    public double? Similarity { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }

    public virtual Artwork Artwork { get; set; } = default!;
}
