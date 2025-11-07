using Application.Models;
using Domain.Enums;

namespace Application.Services.Abstractions;

public interface IRequestService
{
    Task<List<RequestVM>> GetAllRequestsAsync();
    Task<RequestVM?> GetRequestByIdAsync(Guid requestId);
    Task<List<RequestVM>> GetRequestsByAudienceIdAsync();
    Task<List<RequestVM>> GetRequestsByCreatorIdAsync();
    Task<List<RequestVM>> GetRequestsByServiceIdAsync(Guid serviceId);
    Task<List<RequestVM>> GetRequestsByChatboxIdIdAsync(Guid chatboxId);
    Task<RequestVM> AddRequestAsync(RequestModel requestModel);
    Task<RequestVM> UpdateRequestStatusAsync(Guid requestId, StateEnum requestStatus);
}
