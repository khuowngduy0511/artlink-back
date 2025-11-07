using Domain.Entitites;

namespace Domain.Models;

public class ProposalByDate
{
    public DateTime Date { get; set; }
    public int Count { get; set; }
    public int? Total { get; set; }
    public double? IncreaseRate { get; set; }
}
public class PercentageCategoryOfProposal
{
    public string Category { get; set; } = default!;
    public double Percentage { get; set; }
}

public class TopCreatorOfProposal
{
    public Account Creator { get; set; } = default!;
    public int TotalProposal { get; set; } //sô lượng được mua
    public double? TotalRevenue { get; set; } // doanh thu
}
public class TopServiceOfCreator
{
    public Service Service { get; set; } = default!;
    public int TotalService { get; set; } //sô lượng được thue
    public double? TotalRevenue { get; set; } // doanh thu
}