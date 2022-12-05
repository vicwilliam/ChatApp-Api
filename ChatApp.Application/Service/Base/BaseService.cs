using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChatApp.Application.Service.Base;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext DbContext;

    protected BaseService(AppDbContext context)
    {
        DbContext = context;
    }

    public IQueryable<TEntity> GetAll()
    {
        var query = DbContext.Set<TEntity>().AsNoTracking();
        return query;
    }

    public Task<TEntity?> SelectById(Guid id)
    {
        var query = DbContext.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);
        return query;
    }

    public async Task<EntityEntry<TEntity>> Insert(TEntity entity)
    {
        var dbEntity = await DbContext.Set<TEntity>().AddAsync(entity);
        var affectedRows = await DbContext.SaveChangesAsync();

        return dbEntity;
    }

    public async Task<bool> Insert(List<TEntity> entities)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(entities);
        int affectedRows = await DbContext.SaveChangesAsync();

        return affectedRows > 0;
    }

    public Task<bool> Update(int id, TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(Guid id, TEntity entity)
    {
        TEntity? currentEntity = await SelectById(id);

        if (currentEntity == null) return false;

        string? currentEntityType = currentEntity.GetType().FullName;

        if (DbContext.Set<TEntity>().Local.All(p => p.GetType().FullName != currentEntityType))
            DbContext.Set<TEntity>().Attach(currentEntity);

        DbContext.Entry(currentEntity).CurrentValues.SetValues(entity);

        if (DbContext.Entry(currentEntity).State == EntityState.Unchanged) return true;

        int affectedRows = await DbContext.SaveChangesAsync();

        return affectedRows > 0;
    }

    public Task<bool> Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}