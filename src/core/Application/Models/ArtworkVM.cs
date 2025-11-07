using Domain.Enums;

namespace Application.Models;

// View model for view details of an artwork
public class ArtworkVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Thumbnail { get; set; } = default!; // url address
    public int ViewCount { get; set; } = default!;
    public int LikeCount { get; set; } = default!;
    public int CommentCount { get; set; } = default!;
    public string Privacy { get; set; } = default!;
    public string State { get; set; } = default!;
    public bool IsLiked { get; set; } = default!;
    public string Note { get; set; } = default!;
    public bool IsAIGenerated { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public AccountBasicInfoVM Account { get; set; } = default!;
    public LicenseTypeVM LicenseType { get; set; } = default!;
    public List<CategoryVM> CategoryList { get; set; } = default!;
    public List<TagVM> TagList { get; set; } = default!;
    public List<SoftwareUsedVM> SoftwareUseds { get; set; } = default!;
    public List<AssetVM>? Assets { get; set; }
    public List<ImageVM> Images { get; set; } = default!;
}

// View model for search results
public class ArtworkPreviewVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Thumbnail { get; set; } = default!;
    public int ViewCount { get; set; } = default!;
    public int LikeCount { get; set; } = default!;
    public int CommentCount { get; set; } = default!;
    public string Privacy { get; set; } = default!;
    public string State { get; set; } = default!;
    public bool IsLiked { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public AccountBasicInfoVM Account { get; set; } = default!;
}

public class ArtworkContainAssetVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Thumbnail { get; set; } = default!;
    public int ViewCount { get; set; } = default!;
    public int LikeCount { get; set; } = default!;
    public int CommentCount { get; set; } = default!;
    public string Privacy { get; set; } = default!;
    public string State { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public List<AssetVM>? Assets { get; set; }
}

public class ArtworkDisplayModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public string Thumbnail { get; set; } = default!;
    public PrivacyEnum Privacy { get; set; } = default!;
    public AccountDisplayModel Author { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public DateTime? LastModificatedOn { get; set; }
}

// View model for moderation
public class ArtworkModerationVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Thumbnail { get; set; } = default!;
    public string ThumbnailName { get; set; } = default!;
    public int ViewCount { get; set; } = default!;
    public int LikeCount { get; set; } = default!;
    public int CommentCount { get; set; } = default!;
    public string Privacy { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Note { get; set; } = default!;
    public bool IsAIGenerated { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public AccountBasicInfoVM Account { get; set; } = default!;
    public LicenseTypeVM LicenseType { get; set; } = default!;
    public List<CategoryVM> CategoryList { get; set; } = default!;
    public List<TagVM> TagList { get; set; } = default!;
    public List<SoftwareUsedVM> SoftwareUseds { get; set; } = default!;
}

// View model detail for moderation
public class ArtworkDetailModerationVM
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Thumbnail { get; set; } = default!;
    public string ThumbnailName { get; set; } = default!;
    public int ViewCount { get; set; } = default!;
    public int LikeCount { get; set; } = default!;
    public int CommentCount { get; set; } = default!;
    public string Privacy { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Note { get; set; } = default!;
    public bool IsAIGenerated { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public AccountBasicInfoVM Account { get; set; } = default!;
    public LicenseTypeVM LicenseType { get; set; } = default!;
    public List<CategoryVM> CategoryList { get; set; } = default!;
    public List<TagVM> TagList { get; set; } = default!;
    public List<SoftwareUsedVM> SoftwareUseds { get; set; } = default!;
    public List<AssetVM>? Assets { get; set; }
    public List<ImageVM> Images { get; set; } = default!;
    public List<CommentVM>? Comments { get; set; }
}