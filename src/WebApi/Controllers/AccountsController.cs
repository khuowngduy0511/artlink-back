using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IAssetService _assetService;
    private readonly IServiceService _serviceService;
    private readonly IArtworkService _artworkService;
    private readonly IReviewService _reviewService;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;

    public AccountsController(
        IAccountService accountService,
        IMapper mapper,
        IClaimService claimService,
        IAssetService assetService,
        IServiceService serviceService,
        IArtworkService artworkService,
        IReviewService reviewService
        )
    {
        _accountService = accountService;
        _mapper = mapper;
        _claimService = claimService;
        _assetService = assetService;
        _serviceService = serviceService;
        _artworkService = artworkService;
        _reviewService = reviewService;
    }

    // GET: api/accounts
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IPagedList<AccountVM>>> GetAccounts([FromQuery] AccountCriteria criteria)
    {
        try
        {
            var accounts = await _accountService.GetAccountsAsync(criteria);
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // GET: api/accounts
    [HttpGet("/api/moderation/accounts")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<ActionResult<IPagedList<AccountModerationVM>>> GetAccountsForModeration([FromQuery] AccountCriteria criteria)
    {
        try
        {
            var accounts = await _accountService.GetAccountsForModerationAsync(criteria);
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // GET: api/accounts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AccountVM>> GetAccount(Guid id)
    {
        try
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            return Ok(account);
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

    // GET: api/moderation/accounts/5
    // get account for moderation by id
    [HttpGet("/api/moderation/accounts/{id}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<ActionResult<AccountModerationVM>> GetAccountForModeration(Guid id)
    {
        try
        {
            var account = await _accountService.GetAccountByIdForModerationAsync(id);
            return Ok(account);
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

    [HttpGet("role-enum")]
    public IActionResult GetAccountRoleEnum()
    {
        var roleEnums = Enum.GetValues(typeof(RoleEnum))
            .Cast<RoleEnum>().Select(r => new { Id = (int)r, Name = r.ToString() });
        return Ok(roleEnums);
    }

    // PUT: api/accounts/5    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> PutAccount(Guid id, AccountModel model)
    {
        try
        {
            // check authorize
            if (!CheckAuthorize(id))
            {
                return Forbid();
            }
            await _accountService.UpdateAccountAsync(id, model);
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

    // POST: api/accounts    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Account>> PostAccount(AccountCreateModel model)
    {
        try
        {
            var account = _mapper.Map<Account>(model);
            await _accountService.CreateAccountAsync(account);
            return CreatedAtAction("GetAccount", new { id = account.Id }, _mapper.Map<AccountVM>(account));
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

    // DELETE: api/accounts/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        try
        {
            await _accountService.DeleteAccountAsync(id);
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

    // PUT: api/accounts/undelete/5
    [HttpPut("undelete/{id}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> UndeleteAccount(Guid id)
    {
        try
        {
            await _accountService.UndeleteAccountAsync(id);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }

        return NoContent();
    }

    // GET: api/accounts/deleted
    [HttpGet("deleted")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<ActionResult<IEnumerable<AccountVM>>> GetDeletedAccounts()
    {
        try
        {
            var accounts = await _accountService.GetDeletedAccountsAsync();

            return Ok(_mapper.Map<List<AccountVM>>(accounts));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // PUT: api/accounts/{id}/password
    [HttpPut("{id}/password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(Guid id, [FromBody] AccountChangePasswordModel model)
    {
        try
        {
            await _accountService.EditPasswordAsync(id, model.OldPassword, model.NewPassword);
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

    // PUT: api/accounts/avatar
    [HttpPut("{id}/avatar")]
    [Authorize]
    public async Task<IActionResult> ChangeAvatar(Guid id, [FromForm] IFormFile avatar)
    {
        try
        {
            if (!CheckAuthorize(id))
            {
                return Forbid();
            }
            await _accountService.EditAvatarAsync(id, avatar);
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

    private bool CheckAuthorize(Guid accountId)
    {
        // check authorize
        var currentRole = _claimService.GetCurrentRole;
        if (!currentRole.Equals(RoleEnum.Moderator.ToString())
            && !currentRole.Equals(RoleEnum.Admin.ToString()))
        {
            if (_claimService.GetCurrentUserId != accountId)
            {
                return false;
            }
        }
        return true;
    }


    // GET: /api/accounts/{account_id}/assets
    [HttpGet("{accountId}/assets")]
    public async Task<IActionResult> GetAssetsOfAccount(Guid accountId, [FromQuery] PagedCriteria criteria)
    {
        try
        {
            var results = await _artworkService.GetArtworksContainAssetsOfAccountAsync(accountId, criteria);
            if (results == null)
                return NotFound();
            return Ok(results);
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

    // GET: /api/accounts/{account_id}/services
    [HttpGet("{accountId}/services")]
    public async Task<IActionResult> GetServicesOfAccount(Guid accountId, [FromQuery] ServiceCriteria criteria)
    {
        try
        {
            var listServiceVM = await _serviceService.GetServicesOfAccountAsync(accountId, criteria);
            return Ok(listServiceVM);
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

    // GET: /api/accounts/{account_id}/artworks
    [HttpGet("{accountId}/artworks")]
    public async Task<IActionResult> GetArtworksByAccount(Guid accountId, [FromQuery] ArtworkModerationCriteria criteria)
    {
        try
        {
            var result = await _artworkService.GetAllArtworksByAccountIdAsync(accountId, criteria);
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

    // GET: api/accounts/hire
    [HttpGet("hire")]
    public async Task<ActionResult<IEnumerable<AccountVM>>> GetHiredAccounts([FromQuery] PagedCriteria pagedCriteria)
    {
        try
        {
            var accounts = await _accountService.GetHiredAccountAsync(pagedCriteria);
            return Ok(accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    // GET: api/accounts/{id}/assets-bought
    [HttpGet("{accountId}/assets-bought")]
    public async Task<IActionResult> GetAssetsBoughtOfAccount(Guid accountId, [FromQuery] PagedCriteria pagedCriteria)
    {
        try
        {
            // check authorize
            if (!CheckAuthorize(accountId))
            {
                return Forbid();
            }
            var assetsBought = await _assetService.GetAssetsBoughtOfAccountAsync(accountId, pagedCriteria);
            return Ok(assetsBought);
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

    // GET: api/accounts/{id}/reviews
    [HttpGet("{accountId}/reviews")]
    public async Task<IActionResult> GetReviewsByAccountId(Guid accountId, [FromQuery] PagedCriteria pagedCriteria)
    {
        try
        {
            var assetsBought = await _reviewService.GetReviewsByAccountIdAsync(accountId, pagedCriteria);
            return Ok(assetsBought);
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

}
