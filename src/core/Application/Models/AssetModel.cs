using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class AssetModel
{
    [Required]
    public Guid ArtworkId { get; set; }
    [MaxLength(150)]
    public string AssetTitle { get; set; } = default!; // ten asset (vd: Bia sach 2023)
    [MaxLength(500)]
    public string Description { get; set; } = default!; // mo ta asset
    public IFormFile File { get; set; } = default!; // file upload
    public double Price { get; set; } = 0;
}

public class AssetEM
{
    [MaxLength(150)]
    public string AssetTitle { get; set; } = default!; // ten asset (vd: Bia sach 2023)
    [MaxLength(500)]
    public string Description { get; set; } = default!; // mo ta asset
    public double Price { get; set; } = 0;
}

public class MultiAssetModel
{
    [Required]
    public Guid ArtworkId { get; set; }
    public List<SingleAssetModel> Assets { get; set; } = default!;
}

public class SingleAssetModel
{
    [MaxLength(150)]
    [Required]
    public string AssetTitle { get; set; } = default!; // ten asset (vd: Bia sach 2023)
    [MaxLength(500)]
    public string Description { get; set; } = default!; // mo ta asset
    public IFormFile File { get; set; } = default!; // file upload
    public double Price { get; set; } = 0;
}