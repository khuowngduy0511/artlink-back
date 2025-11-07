using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class BlockModel
{
    [Required]
    public Guid BlockedId { get; set; }
}

public class BlockVM
{
    public AccountVM Blocking { get; set; } = default!;
    public AccountVM Blocked { get; set; } = default!;
}

// VM hien thi danh sach account ma user dang block
public class BlockingVM
{
    public Guid BlockerId { get; set; } = default!;
    public List<AccountBasicInfoVM> Blockings { get; set; } = default!;
}

// VM hien thi danh sach account dang block user 
public class BlockerVM
{
    public Guid BlockingId { get; set; } = default!;
    public List<AccountBasicInfoVM> Blockers { get; set; } = default!;
}

public class BlockingListVM
{
    public Guid Blocking { get; set; } = default!;
    public List<AccountVM> Blockeds { get; set; } = default!;
}