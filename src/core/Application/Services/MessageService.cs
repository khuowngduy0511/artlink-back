using Application.Commons;
using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using Application.Services.GoogleStorage;
using AutoMapper;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class MessageService : IMessageService
{
    private static readonly string PARENT_FOLDER = "Message";

    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly IMapper _mapper;
    private readonly ICloudStorageService _cloudStorageService;


    public MessageService(
        IUnitOfWork unitOfWork,
        IClaimService claimService,
        IMapper mapper,
        ICloudStorageService cloudStorageService)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;
        _mapper = mapper;
        _cloudStorageService = cloudStorageService;
    }

    public async Task<List<MessageVM>> GetAllMessageAsync(Guid chatId)
    {
        var messages = await _unitOfWork.MessageRepository
                                .GetMessageByChatBoxIdAsync(chatId);
        return _mapper.Map<List<MessageVM>>(messages);
    }

    public async Task<PagedList<MessageVM>> GetAllMessagePaginationAsync(Guid chatId, PagedCriteria pagecriteria)
    {
        var messagesPagination = await _unitOfWork.MessageRepository
            .GetMessageByChatBoxPaginationAsync(chatId, pagecriteria.PageNumber, pagecriteria.PageSize);
        var viewModel = _mapper.Map<PagedList<MessageVM>>(messagesPagination);
        return viewModel;
    }

    public async Task<MessageVM> SendMessageAsync(MessageModel model)
    {
        // check if message model is valid
        if (model.Text.IsNullOrEmpty() && model.File == null)
        { // text & file location can not be null at the same time
            throw new KeyNotFoundException("Tin nhắn và tệp tin không thể trống cùng lúc.");
        }
        var accountExist = await _unitOfWork.AccountRepository.IsExistedAsync(model.ReceiverId);
        if (!accountExist)
        {
            throw new KeyNotFoundException("Không tìm thấy tải khoản người nhận.");
        }
        var currentUserId = _claimService.GetCurrentUserId ?? default;
        if (model.ReceiverId == currentUserId)
        {
            throw new KeyNotFoundException("Bạn không thể gửi tin nhắn cho chính bạn.");
        }
        var newMessage = _mapper.Map<Message>(model);
        // dat lai ten file
        if (model.File is not null)
        {
            string newMessageName = $"{Path.GetFileNameWithoutExtension(model.File.FileName)}_{DateTime.Now.Ticks}";
            string folderName = PARENT_FOLDER;
            string fileExtension = Path.GetExtension(model.File.FileName);
            // upload file len firebase
            var url = await _cloudStorageService.UploadFileToCloudStorage(model.File, newMessageName, folderName)
                ?? throw new KeyNotFoundException("Lỗi khi tải tệp tin lên đám mây.");
            newMessage.FileLocation = url;
            newMessage.FileName = newMessageName + fileExtension;
        }

        // check if the chatbox is existed
        var senderId = _claimService.GetCurrentUserId ?? default;
        var chatBoxExist = await _unitOfWork.ChatBoxRepository.GetChatBoxAsync(senderId, model.ReceiverId);
        if (chatBoxExist is not null)
        {
            newMessage.ChatBoxId = chatBoxExist.Id;
        }
        else
        {
            ChatBox newChatBox = new()
            {
                AccountId_1 = senderId,
                AccountId_2 = model.ReceiverId,
            };
            await _unitOfWork.ChatBoxRepository.AddAsync(newChatBox);
            newMessage.ChatBoxId = newChatBox.Id;
        }
        // save new message to db
        await _unitOfWork.MessageRepository.AddAsync(newMessage);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<MessageVM>(newMessage);
    }
}
