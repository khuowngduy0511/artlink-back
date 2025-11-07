using Application.Models;

namespace Application.Services.Abstractions;

public interface ISoftwareUsedService
{
    Task<List<SoftwareUsedVM>> GetAllSoftwareUsedAsync();
    Task<SoftwareUsedVM?> GetSoftwareUsedByIdAsync(Guid softwareUsedId);
}
