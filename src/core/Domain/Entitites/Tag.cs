using Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class Tag : BaseEntity
{
    [MaxLength(150)]
    public string TagName { get; set; } = default!;

    public virtual ICollection<TagDetail> TagDetails { get; set; } = new List<TagDetail>();
}
