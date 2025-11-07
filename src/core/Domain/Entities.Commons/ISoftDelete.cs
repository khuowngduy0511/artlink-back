namespace Domain.Entities.Commons;

public interface ISoftDelete
{
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
}
