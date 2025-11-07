using Application.Commons;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Repositories;
public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
{
    public NotificationRepository(AppDBContext dBContext) : base(dBContext)
    {
    }

    public async Task AddNotificationAsync(Notification notification)
    {
        notification.CreatedOn = CurrentTime.GetCurrentTime;
        await _dbContext.Notifications.AddAsync(notification);
    }

    public async Task AddRangeNotificationAsync(List<Notification> listNotification)
    {
        listNotification.ForEach(x => x.CreatedOn = CurrentTime.GetCurrentTime);
        await _dbContext.Notifications.AddRangeAsync(listNotification);
    }

    public async Task<IPagedList<Notification>> GetNotificationOfAccountAsync(Guid accountId, int pageNumer, int pageSize)
    {
        var notifications = _dbContext.Notifications
            .Where(x => x.SentToAccount == accountId)
            .OrderByDescending(x => x.CreatedOn);
        var result = await this.ToPaginationAsync(notifications, pageNumer, pageSize);
        return result;
    }
}