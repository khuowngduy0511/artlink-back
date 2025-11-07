using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;

    public TagsController(ITagService tagService, IMapper mapper)
    {
        _tagService = tagService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTags()
    {
        try
        {
            var result = await _tagService.GetAllTagsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("/api/v2/tags")]
    public async Task<IActionResult> GetTags([FromQuery] BaseCriteria criteria)
    {
        try
        {
            var result = await _tagService.GetTagsAsync(criteria);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // tra ve danh sach tag theo keyword (toi da 20 tag)
    [HttpGet("search/{keyword}")]
    public async Task<IActionResult> SearchTagsByName(string keyword)
    {
        try
        {
            var result = await _tagService.SearchTagsByNameAsync(keyword);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("{tagId}")]
    public async Task<IActionResult> GetTagById(Guid tagId)
    {
        try
        {
            var result = await _tagService.GetTagByIdAsync(tagId);
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

    [HttpDelete("{tagId}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> DeleteTag(Guid tagId)
    {
        try
        {
            await _tagService.DeleteTagAsync(tagId);
            return NoContent();
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
    public async Task<IActionResult> AddTag([FromBody] TagModel tagModel)
    {
        tagModel.TagName = tagModel.TagName.Trim();
        if (!_tagService.IsTagValid(tagModel.TagName))
            return BadRequest(new ApiResponse
            {
                ErrorMessage = $"Tên thẻ '{tagModel.TagName}' không phù hợp (chỉ bao gồm chữ in thường, in hoa, chữ số, khoảng cách, 2-30 kí tự)."
            });
        try
        {
            TagVM tagVM = await _tagService.AddTagAsync(tagModel);
            return CreatedAtAction(nameof(GetTagById), new { tagId = tagVM.Id }, tagVM);
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

    [HttpPut("{tagId}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> EditTag(Guid tagId, [FromBody] TagModel tagModel)
    {
        tagModel.TagName = tagModel.TagName.Trim();
        if (!_tagService.IsTagValid(tagModel.TagName))
            return BadRequest(new ApiResponse
            {
                ErrorMessage = $"Tên thẻ '{tagModel.TagName}' không phù hợp (chỉ bao gồm chữ in thường, in hoa, chữ số, khoảng cách, 2-30 kí tự)."
            });

        try
        {
            await _tagService.EditTagAsync(tagId, tagModel);
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
}
