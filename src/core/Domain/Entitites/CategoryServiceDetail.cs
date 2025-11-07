namespace Domain.Entitites;

public class CategoryServiceDetail
{
    public Guid CategoryId { get; set; }
    public Guid ServiceId { get; set; }

    public virtual Category Category { get; set; } = default!;
    public virtual Service Service { get; set; } = default!;
}
