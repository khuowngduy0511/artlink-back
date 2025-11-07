namespace Domain.Entitites;

public class Block
{
    public Guid BlockingId { get; set; }
    public Guid BlockedId { get; set; }

    public virtual Account Blocking { get; set; } = default!;
    public virtual Account Blocked { get; set; } = default!;
}
