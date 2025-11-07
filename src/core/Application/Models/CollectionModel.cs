using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class CollectionCreationModel
{
    [Required]
    public string CollectionName { get; set; } = default!;
    [Required]
    public PrivacyEnum Privacy { get; set; } = PrivacyEnum.Public;
    [Required]
    public Guid ArtworkId { get; set; }
}

public class CollectionModificationModel
{
    [Required]
    public string CollectionName { get; set; } = default!;
    [Required]
    public PrivacyEnum Privacy { get; set; }
}
