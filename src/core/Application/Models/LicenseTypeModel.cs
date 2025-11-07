namespace Application.Models;

public class LicenseTypeModel
{
    public string LicenseName { get; set; } = default!;
    public string? LicenseDescription { get; set; }
}

public class LicenseTypeVM
{
    public Guid Id { get; set; }
    public string LicenseName { get; set; } = default!;
    public string? LicenseDescription { get; set; }
}