using System.Linq.Expressions;

namespace InternshipTask.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity? GetById (int id);
    IQueryable<TEntity> GetAll();  
    ValueTask<TEntity> CreateAsync(TEntity entity);
    ValueTask<TEntity> AddAsync(TEntity entity);
    ValueTask AddRange(IEnumerable<TEntity> entities);
    ValueTask<TEntity> Remove(TEntity entity);
    ValueTask RemoveRange(IEnumerable<TEntity> entities);
    ValueTask<TEntity> Update(TEntity entity);
    ValueTask<int> SaveChanges(string userId);
}