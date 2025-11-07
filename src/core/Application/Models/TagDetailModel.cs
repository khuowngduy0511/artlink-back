using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class TagDetailModel
{
    public Guid ArtworkId { get; set; }
    [Required]
    [MaxLength(30)]
    public string TagName { get; set; } = default!;
}

public class TagListArtworkModel
{
    public Guid ArtworkId { get; set; }
    public List<string> TagList { get; set; } = default!;
}

public class TagDetailVM
{
    public Guid ArtworkId { get; set; }
    public TagVM Tag { get; set; } = default!;
}

public class TagListArtworkVM
{
    public Guid ArtworkId { get; set; }
    public List<TagVM> TagList { get; set; } = default!;
}

