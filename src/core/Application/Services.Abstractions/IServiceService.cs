using Application.Filters;
using Application.Models;
using Domain.Entities.Commons;

namespace Application.Services.Abstractions;

public interface IServiceService
{
    Task<ServiceVM?> GetServiceByIdAsync(Guid serviceId);
    Task<IPagedList<ServiceVM>> GetAllServicesAsync(ServiceCriteria criteria);
    Task<IPagedList<ServiceVM>> GetServicesOfAccountAsync(Guid accountId, ServiceCriteria criteria);
    Task DeleteServiceAsync(Guid serviceId);
    Task<ServiceVM> AddServiceAsync(ServiceModel serviceModel);
    Task UpdateServiceAsync(Guid serviceId, ServiceEM serviceEM);
}
