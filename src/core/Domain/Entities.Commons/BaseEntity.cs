using MassTransit;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Commons;

public class BaseEntity : BaseEntity<Guid>
{
    public BaseEntity()
    {
        Id = NewId.Next().ToGuid();
    }
}

public class BaseEntity<TId>
{
    [Key]
    public TId Id { get; set; } = default!;
}
