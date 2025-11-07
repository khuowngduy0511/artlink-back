using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class CategoryModel
{
    [MaxLength(50)]
    [Required]
    public string CategoryName { get; set; } = default!;
    public Guid? ParentCategory { get; set; }
}

public class CategoryEM
{
    [MaxLength(50)]
    [Required]
    public string CategoryName { get; set; } = default!;
}

public class CategoryVM
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public CategoryVM? Parent { get; set; }

}

