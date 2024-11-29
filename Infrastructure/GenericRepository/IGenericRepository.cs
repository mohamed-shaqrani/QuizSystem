using System.Linq.Expressions;

namespace Infrastructure.GenericRepository;
public interface IGenericRepository<Entity> where Entity : class
{
    Task<Entity> GetById(int id);
    Task<IEnumerable<Entity>> GetAllAsync();
    Task<IEnumerable<Entity>> GetAllAsyncWithSpecefic(Expression<Func<Entity, bool>> expression);
    IQueryable<Entity> AsQuerable();
    IQueryable<IEnumerable<Entity>> AsQuerableAll();

    Task AddAsync(Entity entity);
    Task AddRange(IEnumerable<Entity> entities);
    Task<bool> Update(Entity updatedEntity);
    Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate);
    IQueryable<Entity> Include(params Expression<Func<Entity, object>>[] includes);

    Task<int> DeleteAsync(Expression<Func<Entity, bool>> expression);
    void SaveInclude(Entity entity, params string[] props);
    void SoftDelete(Entity entity);

}
