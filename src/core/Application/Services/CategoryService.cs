using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Services;
public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<CategoryVM>> GetAllCategoriesAsync()
    {
        var listCate = await _unitOfWork.CategoryRepository.GetAllAsync();
        var listCateVM = _mapper.Map<List<CategoryVM>>(listCate);
        return listCateVM;
    }

    public async Task<CategoryVM?> GetCategoryByIdAsync(Guid categoryId)
    {
        var cate = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(categoryId);
        if (cate == null)
            throw new KeyNotFoundException("Không tìm thấy thể loại.");
        var cateVM = _mapper.Map<CategoryVM>(cate);
        return cateVM;
    }
    public async Task<CategoryVM> AddCategoryAsync(CategoryModel categoryModel)
    {
        // category ko dc trung ten, ko phan biet hoa thuong
        var category = await _unitOfWork.CategoryRepository.
            GetSingleByConditionAsync(x => x.CategoryName.ToLower()
            .Equals(categoryModel.CategoryName.ToLower()));
        if (category != null)
        {
            throw new BadHttpRequestException("Thể loại này đã tồn tại.");
        }

        // neu co category cha, dam bao rang category co ton tai
        // dam bao rang category cha ko phai la sub category
        if (categoryModel.ParentCategory != null)
        {
            Guid parentId = categoryModel.ParentCategory ?? default;
            var parentCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(parentId);

            if (parentCategory == null)
            {
                throw new KeyNotFoundException("Thể loại cha không tồn tại.");
            }

            if (parentCategory.ParentCategory != null)
            {
                throw new BadHttpRequestException("Không thể thêm thể loại này vào thể loại con.");
            }
        }

        var newCategory = _mapper.Map<Category>(categoryModel);
        await _unitOfWork.CategoryRepository.AddAsync(newCategory);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CategoryVM>(newCategory);
    }

    public async Task DeleteCategoryAsync(Guid categoryId)
    {
        var result = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
        if (result == null)
            throw new KeyNotFoundException("Không tìm thấy thể loại.");
        _unitOfWork.CategoryRepository.Delete(result);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Guid categoryId, CategoryEM categoryEM)
    {
        var oldCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
        if (oldCategory == null)
            throw new KeyNotFoundException("Không tìm thấy thể loại.");

        // category ko dc trung ten, ko phan biet hoa thuong
        // loai tru trung ten voi ten cu
        var category = await _unitOfWork.CategoryRepository.
            GetSingleByConditionAsync(x => x.CategoryName.ToLower()
            .Equals(categoryEM.CategoryName.ToLower()));
        if (category != null && !oldCategory.CategoryName.ToLower().Equals(categoryEM.CategoryName.ToLower()))
        {
            throw new BadHttpRequestException("Thể loại này đã tồn tại.");
        }

        oldCategory.CategoryName = categoryEM.CategoryName;

        _unitOfWork.CategoryRepository.Update(oldCategory);
        await _unitOfWork.SaveChangesAsync();
    }
}
