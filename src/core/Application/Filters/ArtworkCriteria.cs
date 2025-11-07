using Domain.Enums;

namespace Application.Filters;

public class ArtworkCriteria : BaseCriteria
{
    public Guid? CategoryId { set; get; }
    public Guid? TagId { set; get; }
}

public class ArtworkElasticCriteria : BaseCriteria
{
    public Guid? CategoryId { set; get; }
    public bool? IsAssetAvailable { set; get; }
    public bool? IsAssetFree { set; get; } 
}

public class RecommendedArtworkCriteria : PagedCriteria
{
    public List<Guid>? ArtworkIds { set; get; }
}


public class ArtworkModerationCriteria : ArtworkCriteria
{
    public StateEnum? State { set; get; }
    public PrivacyEnum? Privacy { set; get; }
}