using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface IRequestRepository : IGenericRepository<Request>
{
    Task<List<Request>> GetRequestsByAudienceIdAsync();
    Task<List<Request>> GetRequestsByCreatorIdAsync();
    Task<List<Request>> GetRequestsByServiceIdAsync(Guid serviceId);
    Task<List<Request>> GetRequestsByChatBoxIdAsync(Guid chatboxId);
}
