
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ChatBoxRepository : GenericRepository<ChatBox>, IChatBoxRepository
{
    public ChatBoxRepository(AppDBContext dBContext) : base(dBContext)
    {
    }

    public async Task<ChatBox?> GetChatBoxAsync(Guid audienceId, Guid creatorId)
    {
        return await _dbContext.ChatBoxes
            .FirstOrDefaultAsync(x =>
            (x.AccountId_1 == audienceId && x.AccountId_2 == creatorId) ||
            (x.AccountId_1 == creatorId && x.AccountId_2 == audienceId));
    }

    public async Task<ChatBox?> GetChatBoxByIdAsync(Guid chatBoxId)
    {
        return await _dbContext.ChatBoxes
                    .Include(x => x.Account_1)
                    .Include(x => x.Account_2)
                    .FirstOrDefaultAsync(x => x.Id == chatBoxId);
    }

    public async Task<List<ChatBox>> GetChatBoxListAsync(Guid accountId)
    {
        return await _dbContext.ChatBoxes
            .Where(x => (x.AccountId_1 == accountId) ||
                        (x.AccountId_2 == accountId))
            .Include(x => x.Account_1)
            .Include(x => x.Account_2)
            .ToListAsync();
    }
}