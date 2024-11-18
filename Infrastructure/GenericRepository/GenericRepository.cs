using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GenericRepository;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected AppDbContext _Context;
    private readonly DbSet<T> _dbSet;


    public GenericRepository(AppDbContext context)
    {
        _Context = context;
        _dbSet = _Context.Set<T>(); // Initialize _dbSet here

    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // Use AsNoTracking for read-only queries for better performance
        return await _dbSet.AsNoTracking().ToListAsync();
    }
    public async Task<IEnumerable<T>> GetAllAsyncWithSpecefic(Expression<Func<T, bool>> expression)
    {
        // Use AsNoTracking for read-only queries for better performance
        return await _dbSet.AsNoTracking().Where(expression).ToListAsync();
    }
    public IQueryable<T> AsQuerable()
         => _Context.Set<T>().AsQueryable();

    public async Task<T> GetById(int id)
        => await _Context.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity)
        => await _Context.Set<T>().AddAsync(entity);

    public async Task<int> DeleteAsync(Expression<Func<T, bool>> expression)
        => await _Context.Set<T>().Where(expression).ExecuteDeleteAsync();

    public async Task<bool> Update(T updatedEntity)
    {
        _Context.Set<T>().Update(updatedEntity);
        return await _Context.SaveChangesAsync() > 0;

    }
    public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return query;
    }
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }


}

