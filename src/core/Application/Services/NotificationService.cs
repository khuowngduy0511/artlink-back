using Application.Commons;
using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddNotificationAsync(NotificationModel model, bool IsSaveChangeAsync = true)
    {
        // check if accountId exist
        var checkAccount = await _unitOfWork.AccountRepository.IsExistedAsync(model.SentToAccount);
        if (!checkAccount)
        {
            throw new KeyNotFoundException("AccountId in SentToAccount of notification not found!");
        }
        // check if references exist
        if (model.ReferencedArtworkId != null)
        {
            var checkReference = await _unitOfWork.ArtworkRepository.IsExistedAsync(model.ReferencedArtworkId.Value);
            if (!checkReference)
            {
                throw new KeyNotFoundException("ArtworkId in notification reference not found!");
            }
        }

        // add new notification & save in db
        var notification = _mapper.Map<Notification>(model);
        await _unitOfWork.NotificationRepository.AddNotificationAsync(notification);
        if (IsSaveChangeAsync)
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task AddRangeNotificationAsync(List<NotificationModel> listModel, bool IsSaveChangeAsync = true)
    {
        // add new notification & save in db
        var listNotification = _mapper.Map<List<Notification>>(listModel);
        await _unitOfWork.NotificationRepository.AddRangeNotificationAsync(listNotification);
        if (IsSaveChangeAsync)
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<PagedList<NotificationVM>> GetNotificationsOfAnAccountAsync(Guid accountId, PagedCriteria pagedCriteria)
    {
        var notificationPagin = await _unitOfWork.NotificationRepository
            .GetNotificationOfAccountAsync(accountId, pagedCriteria.PageNumber, pagedCriteria.PageSize);
        var viewModel = _mapper.Map<PagedList<NotificationVM>>(notificationPagin);
        return viewModel;
    }

    public async Task<NotificationVM> UpdateSeenStateOfNotification(Guid notificationId)
    {
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(notificationId);
        if (notification == null)
        {
            throw new KeyNotFoundException("Notification was not found.");
        }
        if (!notification.IsSeen)
        {
            notification.IsSeen = true;
        }
        else
        {
            notification.IsSeen = false;
        }
        // update db
        _unitOfWork.NotificationRepository.Update(notification);
        await _unitOfWork.SaveChangesAsync();

        var viewModel = _mapper.Map<NotificationVM>(notification);
        return viewModel;
    }
}
