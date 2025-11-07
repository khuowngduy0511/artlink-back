using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ImageModel
{
    [Required]
    public Guid ArtworkId { get; set; }
    public IFormFile Image { get; set; } = default!; // image upload
}

public class MultiImageModel
{
    public Guid ArtworkId { get; set; }
    public List<IFormFile> Images { get; set; } = default!;
}