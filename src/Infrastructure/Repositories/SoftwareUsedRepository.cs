using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Repositories;

public class SoftwareUsedRepository : GenericRepository<SoftwareUsed>, ISoftwareUsedRepository
{
    public SoftwareUsedRepository(AppDBContext dBContext) : base(dBContext)
    {
    }
}
