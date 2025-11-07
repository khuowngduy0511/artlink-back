using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SoftwareDetailRepository : ISoftwareDetailRepository
{
    private readonly AppDBContext _dbContext;

    public SoftwareDetailRepository(AppDBContext dBContext)
    {
        _dbContext = dBContext;
    }
    public async Task AddSoftwareDetailAsync(SoftwareDetail softwareDetail)
    {
        await _dbContext.SoftwareDetails.AddAsync(softwareDetail);
    }

    public void DeleteSoftwareDetail(SoftwareDetail softwareDetail)
    {
        _dbContext.SoftwareDetails.Remove(softwareDetail);
    }

    public Task<List<SoftwareDetail>> GetAllSoftwareDetailsOfSoftwareAsync(Guid artworkId)
    {
        return _dbContext.SoftwareDetails
            .Where(x => x.ArtworkId == artworkId)
            .Include(x => x.SoftwareUsed)
            .ToListAsync();
    }
}
