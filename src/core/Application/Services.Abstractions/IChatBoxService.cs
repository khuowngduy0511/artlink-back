using Application.Models;

namespace Application.Services.Abstractions;

public interface IChatBoxService
{
    Task<ChatBoxVM> GetChatBoxByIdAsync(Guid chatBoxId);
    Task<List<ChatBoxVM>> GetAllChatBoxByAccountIdAsync(Guid accountId);
}
