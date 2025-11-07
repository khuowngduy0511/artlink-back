namespace Domain.Entitites;

public class Follow
{
    public Guid FollowingId { get; set; } // Id of the account that is following another account (chu dong)
    public Guid FollowedId { get; set; } // Id of the account that is being followed (bi dong)

    public virtual Account Following { get; set; } = default!;
    public virtual Account Followed { get; set; } = default!;
}