using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Repositories;
public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
{
    public WalletRepository(AppDBContext dBContext) : base(dBContext)
    {
    }
}
