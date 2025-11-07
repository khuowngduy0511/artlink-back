using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class CategoryServiceDetailService : ICategoryServiceDetailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryServiceDetailService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddCategoryServiceAsync(CategoryServiceModel categoryServiceModel)
    {
        bool IsCategoryExist = await _unitOfWork.CategoryRepository.IsExistedAsync(categoryServiceModel.CategoryId);
        if (!IsCategoryExist)
        {
            throw new KeyNotFoundException("Không tìm thấy thể loại.");
        }
        CategoryServiceDetail categoryServiceDetail = _mapper.Map<CategoryServiceDetail>(categoryServiceModel);
        await _unitOfWork.CategoryServiceDetailRepository.AddCategoryServiceAsync(categoryServiceDetail);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddCategoryListServiceAsync(CategoryListServiceModel categoryListServiceModel, bool isSaveChanges = true)
    {
        var artwork = _unitOfWork.ServiceRepository.GetByIdAsync(categoryListServiceModel.ServiceId);
        if (artwork == null)
        {
            throw new KeyNotFoundException("Không tìm thấy dịch vụ.");
        }

        var categoryList = categoryListServiceModel.CategoryList;
        foreach (var category in categoryList)
        {
            CategoryServiceModel categoryServiceModel = new()
            {
                ServiceId = categoryListServiceModel.ServiceId,
                CategoryId = category
            };
            await AddCategoryServiceAsync(categoryServiceModel);
        }
        if (isSaveChanges)
            await _unitOfWork.SaveChangesAsync();
    }

    public Task DeleteCategoryServiceAsync(Guid categoryServiceId)
    {
        throw new NotImplementedException();
    }
}
