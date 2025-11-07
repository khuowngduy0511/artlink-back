using Application.Commons;
using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Commons;

public class GenericAuditableRepository<TEntity> : GenericCreationRepository<TEntity>
                where TEntity : BaseEntity, ICreation, IModification, ISoftDelete
{
    public GenericAuditableRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public override void Update(TEntity entity)
    {
        entity.LastModificatedOn = CurrentTime.GetCurrentTime;
        entity.LastModificatedBy = _claimService.GetCurrentUserId;
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public override void UpdateRange(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.LastModificatedOn = CurrentTime.GetCurrentTime;
            entity.LastModificatedBy = _claimService.GetCurrentUserId;
        }
        _dbSet.UpdateRange(entities);
    }

    public override void SoftDelete(TEntity entity)
    {
        DateTime dateTime = CurrentTime.GetCurrentTime;
        entity.DeletedOn = dateTime;
        entity.DeletedBy = _claimService.GetCurrentUserId;

        entity.LastModificatedOn = dateTime;
        entity.LastModificatedBy = _claimService.GetCurrentUserId;
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public override async Task<List<TEntity>> GetAllUndeletedAsync()
    {
        return await _dbSet.Where(x => x.DeletedOn == null).OrderByDescending(x => x.CreatedOn).ToListAsync();
    }
}
