using Application.Commons;
using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Commons;

public class GenericCreationRepository<TEntity> : GenericRepository<TEntity>
                                        where TEntity : BaseEntity, ICreation
{
    protected readonly IClaimService _claimService;
    public GenericCreationRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext)
    {
        _claimService = claimService;
    }

    public override async Task AddAsync(TEntity entity)
    {
        entity.CreatedOn = CurrentTime.GetCurrentTime;
        entity.CreatedBy = _claimService.GetCurrentUserId;
        await _dbSet.AddAsync(entity);
    }

    public override async Task AddRangeAsync(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.CreatedOn = CurrentTime.GetCurrentTime;
            entity.CreatedBy = _claimService.GetCurrentUserId;
        }
        await _dbSet.AddRangeAsync(entities);
    }

    public override async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.OrderByDescending(x => x.CreatedOn)
                    .ToListAsync();
    }

    public override async Task<List<TEntity>> GetListByConditionAsync(Expression<Func<TEntity, bool>> query)
    {
        return (await base.GetListByConditionAsync(query))
                .OrderByDescending(x => x.CreatedOn)
                .ToList();
    }
}
