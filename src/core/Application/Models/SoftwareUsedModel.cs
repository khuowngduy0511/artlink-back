namespace Application.Models;

public class SoftwareUsedModel
{
    public string SoftwareName { get; set; } = default!;
}

public class SoftwareUsedVM
{
    public Guid Id { get; set; }
    public string SoftwareName { get; set; } = default!;
}