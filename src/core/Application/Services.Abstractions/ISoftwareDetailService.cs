using Application.Models;

namespace Application.Services.Abstractions;

public interface ISoftwareDetailService
{
    Task AddSoftwareDetailAsync(SoftwareDetailModel model);
    Task DeleteSoftwareDetailAsync(SoftwareDetailModel model);
    Task AddSoftwareListArtworkAsync(SoftwareListArtworkModel model, bool isSaveChanges = true);
}
