namespace Domain.Entities.Commons;

public interface IModification
{
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
}
