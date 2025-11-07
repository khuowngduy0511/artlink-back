using Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class SoftwareUsed : BaseEntity
{

    [MaxLength(150)]
    public string SoftwareName { get; set; } = default!;
    public virtual ICollection<SoftwareDetail> SoftwareDetails { get; set; } = new List<SoftwareDetail>();

}
