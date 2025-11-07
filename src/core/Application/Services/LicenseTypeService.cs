using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class LicenseTypeService : ILicenseTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LicenseTypeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<LicenseTypeVM>> GetAllLicenseTypesAsync()
    {
        var licenseTypeList = await _unitOfWork.LicenseTypeRepository.GetAllAsync();
        var licenseTypeVMList = _mapper.Map<List<LicenseTypeVM>>(licenseTypeList);
        return licenseTypeVMList;
    }

    public async Task<LicenseTypeVM?> GetLicenseTypeByIdAsync(Guid licenseTypeId)
    {
        var licenseType = await _unitOfWork.LicenseTypeRepository.GetByIdAsync(licenseTypeId);
        if (licenseType == null)
            throw new KeyNotFoundException("Không tìm thấy giấy phép.");
        var licenseTypeVM = _mapper.Map<LicenseTypeVM>(licenseType);
        return licenseTypeVM;
    }
}
