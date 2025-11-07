using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class LikeModel
{
    [Required]
    public Guid ArtworkId { get; set; }
}

public class LikeVM
{
    public AccountVM Account { get; set; } = default!;
    public ArtworkVM Artwork { get; set; } = default!;
}

public class AccountLikeVM
{
    public Guid ArtworkId { get; set; }
    public List<AccountBasicInfoVM> AccountLikeds { get; set; } = default!;
}

public class ArtworkLikeVM
{
    public Guid AccountId { get; set; }
    public List<ArtworkPreviewVM> ArtworkLikeds { get; set; } = default!;
}

public class IsLikedVM
{
    public Guid ArtworkId { get; set; } = default!;
    public bool IsLiked { get; set; } = default!;
}