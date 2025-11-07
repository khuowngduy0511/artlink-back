using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class BookmarkModel
{
    [Required]
    public Guid ArtworkId { get; set; }
}
