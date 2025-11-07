namespace Application.Models;

public class TopCreatorOfAssetTransVM
{
    public AccountBasicInfoVM Creator { get; set; } = default!;
    public int TotalBought { get; set; } //sô lượng được mua
    public double? TotalRevenue { get; set; } // doanh thu
}

public class TopCreatorOfProposalVM
{
    public AccountBasicInfoVM Creator { get; set; } = default!;
    public int TotalProposal { get; set; } // tổng số proposal
    public double? TotalRevenue { get; set; } // doanh thu
}
public class TopServiceOfCreatorVM
{
    public ServiceBasicInfoVM Service { get; set; } = default!;
    public int TotalService { get; set; } //sô lượng được thue
    public double? TotalRevenue { get; set; } // doanh thu
}