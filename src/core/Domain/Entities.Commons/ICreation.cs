namespace Domain.Entities.Commons;

public interface ICreation
{
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}