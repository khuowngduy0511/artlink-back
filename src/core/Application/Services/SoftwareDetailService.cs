using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class SoftwareDetailService : ISoftwareDetailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SoftwareDetailService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddSoftwareDetailAsync(SoftwareDetailModel model)
    {
        bool IsSoftwareExist = await _unitOfWork.SoftwareUsedRepository.IsExistedAsync(model.SoftwareUsedId);
        if (!IsSoftwareExist)
        {
            throw new KeyNotFoundException("Không tìm thấy phần mềm.");
        }
        SoftwareDetail softwareDetail = _mapper.Map<SoftwareDetail>(model);
        await _unitOfWork.SoftwareDetailRepository.AddSoftwareDetailAsync(softwareDetail);
        await _unitOfWork.SaveChangesAsync();
    }

    public Task DeleteSoftwareDetailAsync(SoftwareDetailModel model)
    {
        throw new NotImplementedException();
    }

    public async Task AddSoftwareListArtworkAsync(SoftwareListArtworkModel model, bool isSaveChanges = true)
    {
        var softwareUsedList = model.SoftwareList;
        foreach (var softwareUsed in softwareUsedList)
        {
            bool IsSoftwareExist = await _unitOfWork.SoftwareUsedRepository.
                IsExistedAsync(softwareUsed);
            if (!IsSoftwareExist)
            {
                throw new KeyNotFoundException("Không tìm thấy phần mềm.");
            }
            SoftwareDetail softwareDetail = new()
            {
                ArtworkId = model.ArtworkId,
                SoftwareUsedId = softwareUsed
            };
            await _unitOfWork.SoftwareDetailRepository.AddSoftwareDetailAsync(softwareDetail);
        }
        if (isSaveChanges)
            await _unitOfWork.SaveChangesAsync();
    }
}
