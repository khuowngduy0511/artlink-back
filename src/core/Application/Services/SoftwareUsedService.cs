using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class SoftwareUsedService : ISoftwareUsedService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SoftwareUsedService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SoftwareUsedVM>> GetAllSoftwareUsedAsync()
    {
        var softwareUsedList = await _unitOfWork.SoftwareUsedRepository.GetAllAsync();
        var softwareUsedVMList = _mapper.Map<List<SoftwareUsedVM>>(softwareUsedList);
        return softwareUsedVMList;
    }

    public async Task<SoftwareUsedVM?> GetSoftwareUsedByIdAsync(Guid softwareUsedId)
    {
        var softwareUsed = await _unitOfWork.SoftwareUsedRepository.GetByIdAsync(softwareUsedId)
            ?? throw new KeyNotFoundException("Không tìm thấy phần mềm sử dụng.");
        var softwareUsedVM = _mapper.Map<SoftwareUsedVM>(softwareUsed);
        return softwareUsedVM;
    }
}
