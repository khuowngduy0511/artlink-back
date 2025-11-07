using Domain.Entities.Commons;
using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface IMessageRepository : IGenericRepository<Message>
{
    Task<IPagedList<Message>> GetMessageByChatBoxPaginationAsync(Guid chatBoxId, int pageNumber, int pageSize);
    Task<List<Message>> GetMessageByChatBoxIdAsync(Guid chatBoxId);
}
