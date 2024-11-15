using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GenericRepository;
public interface IGenericRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<bool> Update(T updatedEntity);
    Task<int> DeleteAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> GetAllQueryable();
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllAsyncWithSpecefic(Expression<Func<T, bool>> expression);

}
