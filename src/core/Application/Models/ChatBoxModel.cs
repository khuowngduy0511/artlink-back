namespace Application.Models;

public class ChatBoxVM
{
    public Guid Id { get; set; }
    public virtual AccountDisplayModel Account_1 { get; set; } = default!;
    public virtual AccountDisplayModel Account_2 { get; set; } = default!;
}
