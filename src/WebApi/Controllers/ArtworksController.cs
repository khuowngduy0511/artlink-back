using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nest;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtworksController : ControllerBase
{
    private readonly IArtworkService _artworkService;
    private readonly ITagService _tagService;
    private readonly ITagDetailService _tagDetailService;
    private readonly IMapper _mapper;

    public ArtworksController(
        IArtworkService artworkService,
        ITagService tagService,
        ITagDetailService tagDetailService,
        IMapper mapper
        )
    {
        _artworkService = artworkService;
        _tagService = tagService;
        _tagDetailService = tagDetailService;
        _mapper = mapper;
    }

    [HttpGet("elastic")]
    public async Task<IActionResult> GetArtworksWithElasticSearch([FromQuery] ArtworkElasticCriteria criteria)
    {
        try
        {
            var result = await _artworkService.SearchArtworksWithElasticSearchAsync(criteria);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("recommendation")]
    public async Task<IActionResult> GetRecommendedArtworks([FromQuery] RecommendedArtworkCriteria criteria)
    {
        try
        {
            var result = await _artworkService.GetRecommenedArtworkAsync(criteria);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetArtworks([FromQuery] ArtworkCriteria criteria)
    {
        try
        {
            var result = await _artworkService.GetArtworksAsync(criteria);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("followings")]
    [Authorize]
    public async Task<IActionResult> GetArtworksOfFollowings([FromQuery] PagedCriteria criteria)
    {
        try
        {
            var result = await _artworkService.GetArtworksOfFollowingsAsync(criteria);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("privacy-enum")]
    public IActionResult GetArtworkPrivacyEnum()
    {
        var privacyEnums = Enum.GetValues(typeof(PrivacyEnum))
                .Cast<PrivacyEnum>().Select(r => new { Id = (int)r, Name = r.ToString() });
        return Ok(privacyEnums);
    }

    [HttpGet("{artworkId}")]
    public async Task<IActionResult> GetArtworkById(Guid artworkId)
    {
        try
        {
            var result = await _artworkService.GetArtworkByIdAsync(artworkId);
            return Ok(result);
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

    [HttpGet("/api/moderation/artworks/{artworkId}")]
    public async Task<IActionResult> GetArtworkByIdForModeration(Guid artworkId)
    {
        try
        {
            var result = await _artworkService.GetArtworkByIdForModerationAsync(artworkId);
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


    [HttpGet("{artworkId}/duplication")]
    public async Task<IActionResult> GetArtworksDuplicate(Guid artworkId)
    {
        try
        {
            var result = await _artworkService.GetArtworksDuplicateAsync(artworkId);
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
    [Authorize]
    public async Task<IActionResult> AddArtwork([FromForm] ArtworkModel artworkModel)
    {
        #region Validate artwork model
        var tagValidationResult = ValidateTag(artworkModel.Tags);
        if (!tagValidationResult.IsSuccess)
        {
            return BadRequest(tagValidationResult);
        }

        var categoryValidationResult = ValidateCategory(artworkModel.Categories);
        if (!categoryValidationResult.IsSuccess)
        {
            return BadRequest(categoryValidationResult);
        }

        var thumbnailValidationResult = ValidateThumbnail(artworkModel.Thumbnail);
        if (!thumbnailValidationResult.IsSuccess)
        {
            return BadRequest(thumbnailValidationResult);
        }

        var imageFilesValidationResult = ValidateImageFiles(artworkModel.ImageFiles);
        if (!imageFilesValidationResult.IsSuccess)
        {
            return BadRequest(imageFilesValidationResult);
        }

        if (artworkModel.AssetFiles != null)
        {
            var assetFilesValidationResult = ValidateAssetFiles(artworkModel.AssetFiles);
            if (!assetFilesValidationResult.IsSuccess)
            {
                return BadRequest(assetFilesValidationResult);
            }
        }
        #endregion

        try
        {
            var result = await _artworkService.AddArtworkAsync(artworkModel);
            return CreatedAtAction(nameof(GetArtworkById),
                new { artworkId = result.Id }, result);
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


    [HttpPut("{artworkId}")]
    [Authorize]
    public async Task<IActionResult> UpdateArtwork(Guid artworkId, [FromForm] ArtworkEM artworkEM)
    {
        #region Validate artwork model
        if (artworkEM.Tags != null)
        {
            var tagValidationResult = ValidateTag(artworkEM.Tags);
            if (!tagValidationResult.IsSuccess)
            {
                return BadRequest(tagValidationResult);
            }
        }

        if (artworkEM.Categories != null)
        {
            var categoryValidationResult = ValidateCategory(artworkEM.Categories);
            if (!categoryValidationResult.IsSuccess)
            {
                return BadRequest(categoryValidationResult);
            }
        }

        if (artworkEM.Thumbnail != null)
        {
            var thumbnailValidationResult = ValidateThumbnail(artworkEM.Thumbnail);
            if (!thumbnailValidationResult.IsSuccess)
            {
                return BadRequest(thumbnailValidationResult);
            }
        }

        if (artworkEM.ImageFiles != null)
        {
            var imageFilesValidationResult = ValidateImageFiles(artworkEM.ImageFiles);
            if (!imageFilesValidationResult.IsSuccess)
            {
                return BadRequest(imageFilesValidationResult);
            }
        }
        #endregion

        try
        {
            await _artworkService.UpdateArtworkAsync(artworkId, artworkEM);
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

    [HttpDelete("{artworkId}")]
    [Authorize]
    public async Task<IActionResult> DeleteArtwork(Guid artworkId)
    {
        try
        {
            await _artworkService.DeleteArtworkAsync(artworkId);
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

    [HttpDelete("{artworkId}/tags/{tagId}")]
    [Authorize]
    public async Task<IActionResult> DeleteTagOfArtwork(Guid artworkId, Guid tagId)
    {
        try
        {
            await _tagDetailService.DeleteTagDetailAsync(artworkId, tagId);
            return NoContent();
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
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

    [HttpDelete("softdelete/{artworkId}")]
    [Authorize]
    public async Task<IActionResult> SoftDeleteArtwork(Guid artworkId)
    {
        try
        {
            Console.WriteLine($"[SoftDeleteArtwork] Received request to delete artwork: {artworkId}");
            await _artworkService.SoftDeleteArtworkAsync(artworkId);
            Console.WriteLine($"[SoftDeleteArtwork] Successfully deleted artwork: {artworkId}");
            return NoContent();

        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"[SoftDeleteArtwork] KeyNotFoundException: {ex.Message} | ArtworkId: {artworkId}");
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"[SoftDeleteArtwork] UnauthorizedAccessException: {ex.Message} | ArtworkId: {artworkId}");
            return StatusCode(403, new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SoftDeleteArtwork] Exception: {ex.Message} | StackTrace: {ex.StackTrace}");
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet, Route("/api/moderation/[controller]")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> GetAllArtworksForModerator([FromQuery] ArtworkModerationCriteria criteria)
    {
        try
        {
            var result = await _artworkService.GetAllArtworksForModerationAsync(criteria);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpPut, Route("/api/moderation/[controller]/{artworkId}/state")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> UpdateArtworkStatusForModerator(Guid artworkId, ArtworkStateEM model)
    {
        try
        {
            await _artworkService.UpdateArtworkStateAsync(artworkId, model);
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

    private ApiResponse ValidateThumbnail(IFormFile thumbnail)
    {
        if (!FileValidationHelper.IsImageFormatValid(thumbnail.FileName))
        {
            return new ApiResponse
            {
                ErrorMessage = "Hình ảnh phải có định dạng: JPG, JPEG, PNG, GIF, BMP, WEBP, or SVG."
            };
        }

        if (!FileValidationHelper.IsAvatarFileSizeValid(thumbnail))
        {
            return new ApiResponse
            {
                ErrorMessage = "Dung lượng ảnh đại diện không quá 5MB."
            };
        }

        return new ApiResponse { IsSuccess = true };
    }

    // so tag toi da la 30, moi tag phai hop le
    private ApiResponse ValidateTag(List<string> Tags)
    {
        if (Tags.Count > 30)
        {
            return new ApiResponse
            {
                ErrorMessage = "Số lượng thẻ không quá 30."
            };
        }

        foreach (var tag in Tags)
        {
            if (!_tagService.IsTagValid(tag))
            {
                return new ApiResponse
                {
                    ErrorMessage = $"Tên thẻ '{tag}' không phù hợp (chỉ bao gồm chữ in thường, in hoa, chữ số, khoảng cách, 2-30 kí tự)."
                };
            }
        }

        return new ApiResponse { IsSuccess = true };
    }
    private ApiResponse ValidateCategory(HashSet<Guid> Categories)
    {
        if (Categories.Count > 3)
        {
            return new ApiResponse
            {
                ErrorMessage = "Số lượng thể loại không quá 3."
            };
        }

        return new ApiResponse { IsSuccess = true };
    }


    private ApiResponse ValidateImageFiles(List<IFormFile> imageFiles)
    {
        if (imageFiles.Count > 30)
        {
            return new ApiResponse
            {
                ErrorMessage = "Số lượng hình ảnh không quá 30."
            };
        }

        foreach (var image in imageFiles)
        {
            if (!FileValidationHelper.IsImageFormatValid(image.FileName))
            {
                return new ApiResponse
                {
                    ErrorMessage = "Hình ảnh phải có định dạng: JPG, JPEG, PNG, GIF, BMP, WEBP, or SVG."
                };
            }

            if (!FileValidationHelper.IsImageFileSizeValid(image))
            {
                return new ApiResponse
                {
                    ErrorMessage = "Dung lượng hình ảnh không quá 16MB."
                };
            }
        }

        return new ApiResponse { IsSuccess = true };
    }

    private ApiResponse ValidateAssetFiles(List<SingleAssetModel> assetFiles)
    {
        {
            if (assetFiles.Count > 5)
            {
                return new ApiResponse
                {
                    ErrorMessage = "Số lượng tài nguyên không vượt quá 5"
                };
            }

            foreach (var asset in assetFiles)
            {
                if (!FileValidationHelper.IsAssetFormatValid(asset.File.FileName))
                {
                    return new ApiResponse
                    {
                        ErrorMessage = "Tệp tin phải có định dạng: ABR, AI, ASE, DNG, DOC, DOCX, EPS, GIF, INDD, JPEG, JPG, OTF, PDF, PNG, PPT, PPTX, PSD, RAW, SVG, TIF, TIFF, TTF, TXT, WEBP, WOFF, WOFF2, XLS, XLSX, XMP, ZIP, RAR."
                    };
                }

                if (!FileValidationHelper.IsAssetFileSizeValid(asset.File))
                {
                    return new ApiResponse
                    {
                        ErrorMessage = "Dung lượng tệp tin không quá 500MB."
                    };
                }
            }
        }

        return new ApiResponse { IsSuccess = true };
    }
}

