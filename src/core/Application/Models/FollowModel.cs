using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class FollowModel
{
    [Required]
    public Guid FollowedId { get; set; }
}

public class FollowingVM
{
    public Guid FollowerId { get; set; } = default!;
    public List<AccountBasicInfoVM> Followings { get; set; } = default!;
}

public class FollowerVM
{
    public Guid FollowingId { get; set; } = default!;
    public List<AccountBasicInfoVM> Followers { get; set; } = default!;
}
