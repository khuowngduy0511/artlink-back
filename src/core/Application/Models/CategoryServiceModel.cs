using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class CategoryServiceModel
{
    [Required]
    public Guid CategoryId { get; set; }
    [Required]
    public Guid ServiceId { get; set; }
}

public class CategoryServiceVM
{
    public Guid ServiceId { get; set; }
    public CategoryVM Category { get; set; } = default!;
}

public class CategoryListServiceModel
{
    [Required]
    public Guid ServiceId { get; set; }
    [Required]
    public List<Guid> CategoryList { get; set; } = default!;
}