using Application.Commons;
using Application.Filters;
using Application.Models;

namespace Application.Services.Abstractions;

public interface INotificationService
{
    Task AddNotificationAsync(NotificationModel model, bool IsSaveChangeAsync = true);
    Task AddRangeNotificationAsync(List<NotificationModel> listModel, bool IsSaveChangeAsync = true);
    Task<PagedList<NotificationVM>> GetNotificationsOfAnAccountAsync(Guid accountId, PagedCriteria pagedCriteria);
    Task<NotificationVM> UpdateSeenStateOfNotification(Guid notificationId);
}
