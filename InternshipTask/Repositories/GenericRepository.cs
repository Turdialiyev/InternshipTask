using System.Linq.Expressions;
using InternshipTask.Data;

namespace InternshipTask.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    public async ValueTask<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);


        return entry.Entity;
    }
    public async ValueTask<TEntity> CreateAsync(TEntity model)
    {
        var entity = await _context.Set<TEntity>().AddAsync(model);


        return entity.Entity;
    }

    public async ValueTask AddRange(IEnumerable<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);

    }

    public IQueryable<TEntity> GetAll()
      => _context.Set<TEntity>();

    public TEntity? GetById(int id)
     => _context.Set<TEntity>().Find(id);

    public async ValueTask<TEntity> Remove(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Remove(entity);


        return entry.Entity;
    }

    public async ValueTask RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);

    }

    public async ValueTask<TEntity> Update(TEntity entity)
    {
        var entry =  _context.Set<TEntity>().Update(entity);

        return entry.Entity;
    }

}