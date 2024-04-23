using System.Linq.Expressions;

namespace Tokobaju.Repositories;

public interface IRepository<TEntity>{
    Task<TEntity> SaveAsync(TEntity entity);
    Task<TEntity?> FindByIdAsync(string id);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria);
    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes);
    Task<List<TEntity>> FindAllAsync();
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria);
    Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes);
    TEntity Attach(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
}