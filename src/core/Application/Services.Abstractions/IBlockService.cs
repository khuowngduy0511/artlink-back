using Application.Models;

namespace Application.Services.Abstractions;

public interface IBlockService
{
    Task CreateBlockAsync(BlockModel blockModel);
    Task DeleteBlockAsync(Guid blockedId);
    Task<BlockingVM> GetBlockingsOfBlockerAsync(Guid blockerId); // lay danh sach account ma user dang block
    Task<BlockerVM> GetBlockersOfBlockingAsync(Guid blockingId); // lay danh sach account dang block user
    Task<BlockVM?> GetBlockByIdAsync(Guid blockingId, Guid blockedId);
}
