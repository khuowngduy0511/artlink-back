using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ServiceDetailRepository : IServiceDetailRepository
{
    private readonly AppDBContext _dbContext;

    public ServiceDetailRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddServiceDetailAsync(ServiceDetail serviceDetail)
    {
        await _dbContext.ServiceDetails.AddAsync(serviceDetail);
    }

    public async Task DeleteAllServiceDetailAsync(Guid serviceId)
    {
        var serviceDetails = await _dbContext.ServiceDetails
            .Where(sd => sd.ServiceId == serviceId).ToArrayAsync();
        if (serviceDetails.Length > 0)
        {
            _dbContext.ServiceDetails.RemoveRange(serviceDetails);
        }
    }
}
