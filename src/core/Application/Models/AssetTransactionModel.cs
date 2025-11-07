using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class AssetTransactionModel
{
    [Required]
    public Guid AssetId { get; set; }
}
