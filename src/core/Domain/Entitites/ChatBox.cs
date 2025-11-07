using Domain.Entities.Commons;

namespace Domain.Entitites;

public class ChatBox : BaseEntity
{
    public Guid AccountId_1 { get; set; }
    public Guid AccountId_2 { get; set; }

    public virtual Account Account_1 { get; set; } = default!;
    public virtual Account Account_2 { get; set; } = default!;
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
