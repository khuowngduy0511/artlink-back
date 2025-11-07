using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class CategoryArtworkDetailService : ICategoryArtworkDetailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryArtworkDetailService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddCategoryArtworkAsync(CategoryArtworkModel categoryArtworkModel)
    {
        bool IsCategoryExist = await _unitOfWork.CategoryRepository.IsExistedAsync(categoryArtworkModel.CategoryId);
        if (!IsCategoryExist)
        {
            throw new KeyNotFoundException("Không tìm thấy thể loại.");
        }
        CategoryArtworkDetail categoryArtworkDetail = _mapper.Map<CategoryArtworkDetail>(categoryArtworkModel);
        await _unitOfWork.CategoryArtworkDetailRepository.AddCategoryArtworkAsync(categoryArtworkDetail);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AddCategoryListArtworkAsync(CategoryListArtworkModel categoryListArtworkModel, bool isSaveChanges = true)
    {
        var artwork = _unitOfWork.ArtworkRepository.GetByIdAsync(categoryListArtworkModel.ArtworkId);
        if (artwork == null)
        {
            throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        }

        var categoryList = categoryListArtworkModel.CategoryList;
        foreach (var category in categoryList)
        {
            CategoryArtworkModel categoryArtworkModel = new()
            {
                ArtworkId = categoryListArtworkModel.ArtworkId,
                CategoryId = category
            };
            await AddCategoryArtworkAsync(categoryArtworkModel);
        }
        if (isSaveChanges)
            await _unitOfWork.SaveChangesAsync();
    }

    public Task DeleteCategoryArtworkAsync(Guid categoryArtworkId)
    {
        throw new NotImplementedException();
    }
}
