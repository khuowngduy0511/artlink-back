using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Repositories;

public class LicenseTypeRepository : GenericRepository<LicenseType>, ILicenseTypeRepository
{
    public LicenseTypeRepository(AppDBContext dBContext) : base(dBContext)
    {
    }
}
