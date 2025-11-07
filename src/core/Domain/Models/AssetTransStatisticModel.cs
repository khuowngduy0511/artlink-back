using Domain.Entitites;

namespace Domain.Models;

public class AssetTransByDate
{
    public DateTime Date { get; set; }
    public int Count { get; set; }
    public int? Total { get; set; }
    public double? IncreaseRate { get; set; }
}

public class PercentageCategoryOfAssetTrans
{
    public string Category { get; set; } = default!;
    public double Percentage { get; set; }
}

public class TopCreatorOfAssetTrans
{
    public Account Creator { get; set; } = default!;
    public int TotalBought { get; set; } //sô lượng được mua
    public double? TotalRevenue { get; set; } // doanh thu
}