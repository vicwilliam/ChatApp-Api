using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChatApp.Application.Service.Base;

public interface IBaseService<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> SelectById(Guid id);

    Task<EntityEntry<TEntity>> Insert(TEntity entity);
    Task<bool> Insert(List<TEntity> entities);

    Task<bool> Update(int id, TEntity entity);
    Task<bool> Update(TEntity entity);

    Task<bool> Delete(TEntity entity);
    Task<bool> DeleteById(int id);
}