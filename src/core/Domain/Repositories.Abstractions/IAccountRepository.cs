using Domain.Entities.Commons;
using Domain.Entitites;

namespace Domain.Repositories.Abstractions;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<IPagedList<Account>> GetAllAccountsAsync(string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task<IPagedList<Account>> GetAllAccountsForModerationAsync(string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task<IPagedList<Account>> GetAllHiredAccountsAsync(int pageNumber, int pageSize);
    Task<Account?> GetAccountDetailAsync(Guid id);
}
