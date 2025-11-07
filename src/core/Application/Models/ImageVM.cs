namespace Application.Models;

public class ImageVM
{
    public Guid Id { get; set; }
    public Guid ArtworkId { get; set; }
    public string ImageName { get; set; } = default!;
    public string Location { get; set; } = default!;
    public int Order { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
}

public class ImageDuplicationVM
{
    public Guid Id { get; set; }
    public Guid ArtworkId { get; set; }
    public string ImageName { get; set; } = default!;
    public string Location { get; set; } = default!;
    public int Order { get; set; }
    public double Similarity { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public ArtworkPreviewVM Artwork { get; set; } = default!;

}