using Domain.Entities.Commons;

namespace Domain.Entitites;
public class Comment : BaseEntity, ICreation, IModification, ISoftDelete
{
    public Guid ArtworkId { get; set; }
    public string Content { get; set; } = default!;
    public Guid? ReplyId { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    public virtual Artwork Artwork { get; set; } = default!;
    public virtual Comment? Reply { get; set; }
    public virtual Account Account { get; set; } = default!;
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();

}
