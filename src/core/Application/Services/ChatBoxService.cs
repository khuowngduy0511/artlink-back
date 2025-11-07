using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class ChatBoxService : IChatBoxService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChatBoxService(IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ChatBoxVM>> GetAllChatBoxByAccountIdAsync(Guid accountId)
    {
        var accountExist = await _unitOfWork.AccountRepository.IsExistedAsync(accountId);
        if (!accountExist)
        {
            throw new KeyNotFoundException("Không tìm thấy tài khoản");
        }
        var chats = await _unitOfWork.ChatBoxRepository.GetChatBoxListAsync(accountId);
        return _mapper.Map<List<ChatBoxVM>>(chats);
    }

    public async Task<ChatBoxVM> GetChatBoxByIdAsync(Guid chatBoxId)
    {
        var chat = await _unitOfWork.ChatBoxRepository.GetChatBoxByIdAsync(chatBoxId);
        return _mapper.Map<ChatBoxVM>(chat);
    }
}
