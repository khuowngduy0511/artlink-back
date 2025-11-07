using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class CategoryArtworkModel
{
    [Required]
    public Guid CategoryId { get; set; }
    [Required]
    public Guid ArtworkId { get; set; }
}

public class CategoryArtworkVM
{
    public Guid ArtworkId { get; set; }
    public CategoryVM Category { get; set; } = default!;
}

public class CategoryListArtworkModel
{
    [Required]
    public Guid ArtworkId { get; set; }
    [Required]
    public HashSet<Guid> CategoryList { get; set; } = default!;
}