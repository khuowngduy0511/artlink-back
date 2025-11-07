using Domain.Entities.Commons;
using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface IServiceRepository : IGenericRepository<Service>
{
    Task<Service?> GetServiceByIdAsync(Guid id);
    Task<IPagedList<Service>?> GetAllServicesAsync(
        Guid? accountId, int? minPrice, int? maxPrice, string? keyword,
        string? sortColumn, string? sortOrder, int page, int pageSize);
    public Task<double> GetAverageRatingOfServiceAsync(Guid serviceId);
}
