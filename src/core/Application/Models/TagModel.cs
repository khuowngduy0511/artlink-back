using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class TagModel
{
    [MaxLength(30)]
    [Required]
    public string TagName { get; set; } = default!;
}

public class TagVM
{
    public Guid Id { get; set; }
    public string TagName { get; set; } = default!;
}