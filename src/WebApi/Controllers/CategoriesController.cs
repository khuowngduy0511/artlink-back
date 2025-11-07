using Application.Models;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        try
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById(Guid categoryId)
    {
        try
        {
            var result = await _categoryService.GetCategoryByIdAsync(categoryId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> AddCategory(CategoryModel categoryModel)
    {
        try
        {
            var category = await _categoryService.AddCategoryAsync(categoryModel);
            return CreatedAtAction(nameof(GetCategoryById),
                new { categoryId = category.Id }, category);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpPut("{categoryId}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, CategoryEM categoryEM)
    {
        try
        {
            await _categoryService.UpdateCategoryAsync(categoryId, categoryEM);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpDelete("{categoryId}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(categoryId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception)
        {
            return BadRequest(new ApiResponse
            {
                ErrorMessage = "Lỗi khi xóa! Nếu bạn đang xóa thể loại chính, " +
                "hãy đảm bảo xóa tất cả các thể loại con trước."
            });
        }
    }
}
