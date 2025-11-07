using System.ComponentModel.DataAnnotations;

namespace Application.Filters;

public class AssetCriteria : BaseCriteria
{
    [Range(0, 90000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int? MinPrice { set; get; }
    [Range(0, 100000000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int? MaxPrice { set; get; }
}
