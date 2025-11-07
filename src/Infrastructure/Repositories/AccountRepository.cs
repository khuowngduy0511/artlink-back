using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AccountRepository : GenericAuditableRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDBContext dBContext, IClaimService claimService)
        : base(dBContext, claimService)
    {
    }

    public async Task<Account?> GetAccountDetailAsync(Guid id)
    {
        var account = await _dbContext.Accounts
            .Include(x => x.Wallet)
            .FirstOrDefaultAsync(x => x.Id == id);
        return account;
    }

    public async Task<IPagedList<Account>> GetAllAccountsAsync(string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var allAccounts = _dbContext.Accounts
            .Where(x => x.DeletedOn == null);

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allAccounts = allAccounts.Where(x => x.Username.ToLower().Contains(keyword)
            || x.Email.ToLower().Contains(keyword) || x.Fullname.ToLower().Contains(keyword));
        }

        #region sorting
        Expression<Func<Account, object>> orderBy = sortColumn?.ToLower() switch
        {
            "created" => x => x.CreatedOn,
            _ => x => x.CreatedOn,
        };

        if (sortOrder?.ToLower() == "asc")
        {
            allAccounts = allAccounts.OrderBy(orderBy);
        }
        else
        {
            allAccounts = allAccounts.OrderByDescending(orderBy);
        }
        #endregion

        #region paging
        var result = await ToPaginationAsync(allAccounts, page, pageSize);
        #endregion

        return result;
    }

    public async Task<IPagedList<Account>> GetAllAccountsForModerationAsync(string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var allAccounts = _dbContext.Accounts
            .Include(x => x.Wallet)
            .Where(x => x.DeletedOn == null);

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allAccounts = allAccounts.Where(x => x.Username.ToLower().Contains(keyword)
            || x.Email.ToLower().Contains(keyword) || x.Fullname.ToLower().Contains(keyword));
        }

        #region sorting
        Expression<Func<Account, object>> orderBy = sortColumn?.ToLower() switch
        {
            "created" => x => x.CreatedOn,
            _ => x => x.CreatedOn,
        };

        if (sortOrder?.ToLower() == "asc")
        {
            allAccounts = allAccounts.OrderBy(orderBy);
        }
        else
        {
            allAccounts = allAccounts.OrderByDescending(orderBy);
        }
        #endregion

        #region paging
        var result = await ToPaginationAsync(allAccounts, page, pageSize);
        #endregion

        return result;
    }

    public async Task<IPagedList<Account>> GetAllHiredAccountsAsync(int pageNumber, int pageSize)
    {
        var accounts = _dbContext.Accounts
            .Where(x => x.DeletedOn == null)
            .Include(x => x.Proposals)
            .Where(x => x.Role == RoleEnum.CommonUser);
        var result = await this.ToPaginationAsync(accounts, pageNumber, pageSize);
        return result;
    }
}
