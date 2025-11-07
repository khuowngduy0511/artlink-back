using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ArtworkEM
{
    [MaxLength(150)]
    public string Title { get; set; } = default!;
    [MaxLength(5000)]
    public string? Description { get; set; }
    public PrivacyEnum Privacy { get; set; } = default!;
    public bool IsAIGenerated { get; set; } = default!;
    public IFormFile? Thumbnail { get; set; }
    public List<IFormFile>? ImageFiles { get; set; }
    public List<string>? Tags { get; set; }
    public HashSet<Guid>? Categories { get; set; }
}

public class ArtworkModel
{
    [MaxLength(150)]
    [Required]
    public string Title { get; set; } = default!;
    [MaxLength(5000)]
    public string? Description { get; set; }
    [Required]
    public IFormFile Thumbnail { get; set; } = default!;
    [Required]
    public PrivacyEnum Privacy { get; set; } = default!;
    public bool IsAIGenerated { get; set; } = default!;
    public Guid? LicenseTypeId { get; set; } = default!;
    [Required]
    public List<IFormFile> ImageFiles { get; set; } = default!;
    public List<SingleAssetModel>? AssetFiles { get; set; }
    [Required]
    public List<string> Tags { get; set; } = default!; // dung hashset de tranh trung lap
    public HashSet<Guid> Categories { get; set; } = default!;
    public HashSet<Guid>? SoftwareUseds { get; set; }
}

public class ArtworkStateEM
{
    [Required]
    public StateEnum State { get; set; } = default!;
    [MaxLength(500)]
    public string? Note { get; set; }
}