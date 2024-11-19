using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Infrastructure.GenericRepository;
public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
{
    protected AppDbContext _Context;
    private readonly DbSet<Entity> _dbSet;


    public GenericRepository(AppDbContext context)
    {
        _Context = context;
        _dbSet = _Context.Set<Entity>(); // Initialize _dbSet here

    }
    public async Task<IEnumerable<Entity>> GetAllAsync()
    {
        // Use AsNoTracking for read-only queries for better performance
        return await _dbSet.AsNoTracking().ToListAsync();
    }
    public async Task<IEnumerable<Entity>> GetAllAsyncWithSpecefic(Expression<Func<Entity, bool>> expression)
    {
        // Use AsNoTracking for read-only queries for better performance
        return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
    }
    public IQueryable<Entity> AsQuerable()
         => _Context.Set<Entity>().AsQueryable();

    public async Task<Entity> GetById(int id)
        => await _Context.Set<Entity>().FirstOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(Entity entity)
        => await _Context.Set<Entity>().AddAsync(entity);

    public async Task<bool> Update(Entity updatedEntity)
    {
        _Context.Set<Entity>().Update(updatedEntity);
        return await _Context.SaveChangesAsync() > 0;

    }
    public IQueryable<Entity> Include(params Expression<Func<Entity, object>>[] includes)
    {
        IQueryable<Entity> query = _dbSet;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }
    public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public void SaveInclude(Entity entity, params string[] props)
    {
        var local = _dbSet.Local.FirstOrDefault(x => x.Id == entity.Id);
        EntityEntry entry = null;
        if (local is null)
            entry = _Context.Entry(entity);

        else
            entry = _Context.ChangeTracker.Entries<Entity>().FirstOrDefault(x => x.Entity.Id == entity.Id);

        foreach (var item in entry.Properties)
        {
            if (props.Contains(item.Metadata.Name))
            {
                item.CurrentValue = entity.GetType().GetProperty(item.Metadata.Name).GetValue(entity);
                item.IsModified = true;
            }
        }
    }
    public async Task<int> DeleteAsync(Expression<Func<Entity, bool>> expression)
       => await _Context.Set<Entity>().Where(expression).ExecuteDeleteAsync();
    public void SoftDelete(Entity entity)
    {
        entity.isDeleted = true;
        SaveInclude(entity, nameof(BaseModel.isDeleted));
    }
}

