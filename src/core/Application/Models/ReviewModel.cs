using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ReviewModel
{
    public Guid ProposalId { get; set; }
    public double Vote { get; set; }
    [MaxLength(150)]
    public string Detail { get; set; } = string.Empty;
}
public class ReviewEM
{
    public double Vote { get; set; }
    [MaxLength(150)]
    public string Detail { get; set; } = string.Empty;
}
