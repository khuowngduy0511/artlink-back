using Domain.Entities.Commons;
using Domain.Enums;

namespace Domain.Entitites;
public class Collection : BaseEntity, ICreation
{
    public string CollectionName { get; set; } = default!;
    public PrivacyEnum Privacy { get; set; }

    public virtual Account Account { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public virtual ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();

}
