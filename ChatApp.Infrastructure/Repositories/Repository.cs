using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Models;
using ChatApp.Infrastructure.Context;

namespace ChatApp.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public TEntity? GetById(Guid id)
    {
        IQueryable<TEntity?> query = _context.Set<TEntity>().Where(e => e.Id == id);
        if (query.Any())
            return query.FirstOrDefault();
        return null;
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        var query = _context.Set<TEntity>();
        if(query.Any())
            return query.ToList();
        return new List<TEntity>();
    }
    public virtual void Save(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);   
    }
}