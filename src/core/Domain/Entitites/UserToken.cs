using Domain.Entities.Commons;

namespace Domain.Entitites;

public class UserToken : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ATid { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public Guid RTid { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public bool IsUsed { get; set; } = false;
    public bool IsRevoked { get; set; } = false;
    public DateTime IssuedDate { get; set; }
    public DateTime ExpiredDate { get; set; }

    public virtual Account User { get; set; } = default!;
}
