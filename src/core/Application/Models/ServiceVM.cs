namespace Application.Models;

public class ServiceVM
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string DeliveryTime { get; set; } = default!;
    public int NumberOfConcept { get; set; } = default!;
    public int NumberOfRevision { get; set; } = default!;
    public double StartingPrice { get; set; } = default!;
    public string Thumbnail { get; set; } = default!;
    public double AverageRating { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }

    public AccountBasicInfoVM Account { get; set; } = default!;
    public List<CategoryVM> Categories { get; set; } = default!;
    public List<ArtworkPreviewVM> ArtworkReferences { get; set; } = default!;
}

public class ServiceBasicInfoVM
{
    public Guid Id { get; set; }
    public string ServiceName { get; set; } = default!;
    public string Description { get; set; } = string.Empty;
    public string DeliveryTime { get; set; } = default!;
    public int NumberOfConcept { get; set; } = default!;
    public int NumberOfRevision { get; set; } = default!;
    public double StartingPrice { get; set; } = default!;
    public string Thumbnail { get; set; } = default!;
    public double AverageRating { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}
