using Application.Models;

namespace Application.Services.Abstractions;

public interface ILicenseTypeService
{
    Task<List<LicenseTypeVM>> GetAllLicenseTypesAsync();
    Task<LicenseTypeVM?> GetLicenseTypeByIdAsync(Guid licenseTypeId);
}
