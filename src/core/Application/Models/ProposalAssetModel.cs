using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ProposalAssetModel
{
    [Required]
    public Guid ProposalId { get; set; }
    public ProposalAssetEnum Type { get; set; }
    public IFormFile File { get; set; } = default!;
}
