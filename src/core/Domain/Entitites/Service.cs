using Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class Service : BaseEntity, ICreation, IModification, ISoftDelete
{
    [MaxLength(150)]
    public string ServiceName { get; set; } = default!;
    [MaxLength(1000)]
    public string Description { get; set; } = default!;
    [MaxLength(150)]
    public string DeliveryTime { get; set; } = default!;
    public int NumberOfConcept { get; set; } = default!;
    public int NumberOfRevision { get; set; } = default!;
    public double StartingPrice { get; set; } = default!;
    public string Thumbnail { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    public virtual Account Account { get; set; } = default!;
    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
    public virtual ICollection<Proposal> Proposals { get; set; } = new List<Proposal>();
    public virtual ICollection<ServiceDetail> ServiceDetails { get; set; } = new List<ServiceDetail>();
    public virtual ICollection<CategoryServiceDetail> CategoryServiceDetails { get; set; } = new List<CategoryServiceDetail>();
}
