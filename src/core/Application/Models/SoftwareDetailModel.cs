using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class SoftwareDetailModel
{
    [Required]
    public Guid ArtworkId { get; set; }
    [Required]
    public Guid SoftwareUsedId { get; set; }
}

public class SoftwareListArtworkModel
{
    [Required]
    public Guid ArtworkId { get; set; }
    [Required]
    public HashSet<Guid> SoftwareList { get; set; } = default!;
}