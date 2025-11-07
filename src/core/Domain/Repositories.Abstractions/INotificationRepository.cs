using Domain.Entities.Commons;
using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface INotificationRepository : IGenericRepository<Notification>
{
    Task AddNotificationAsync(Notification notification);
    Task AddRangeNotificationAsync(List<Notification> listNotification);
    Task<IPagedList<Notification>> GetNotificationOfAccountAsync(Guid accountId, int pageNumer, int pageSize);
}
