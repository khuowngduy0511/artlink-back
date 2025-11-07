using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class RequestRepository : GenericCreationRepository<Request>, IRequestRepository
{
    public RequestRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<List<Request>> GetRequestsByAudienceIdAsync()
    {
        return await _dbContext.Requests
            .Include(x => x.Service)
            .Include(x => x.Account)
            .Where(x => x.CreatedBy == _claimService.GetCurrentUserId)
            .ToListAsync();
    }

    public async Task<List<Request>> GetRequestsByCreatorIdAsync()
    {
        return await _dbContext.Requests
            .Include(x => x.Service)
            .Include(x => x.Account)
            .Where(x => x.Service.CreatedBy == _claimService.GetCurrentUserId)
            .ToListAsync();
    }

    public async Task<List<Request>> GetRequestsByChatBoxIdAsync(Guid chatboxId)
    {
        return await _dbContext.Requests
            .Include(x => x.Service)
            .Include(x => x.MessageObj)
            .Where(x => x.MessageObj.ChatBoxId == chatboxId)
            .ToListAsync();
    }

    public async Task<List<Request>> GetRequestsByServiceIdAsync(Guid serviceId)
    {
        return await _dbContext.Requests
            .Include(x => x.Service)
            .Where(x => x.Service.Id == serviceId)
            .ToListAsync();
    }
}
