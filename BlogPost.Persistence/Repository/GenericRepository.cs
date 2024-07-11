global using System.Linq.Expressions;
global using BlogPost.Application.Contract.Interface;

namespace BlogPost.Persistence.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly BlogPostContext _context;

    public GenericRepository(BlogPostContext context)
    {
        _context = context;
    }

    public void Add(T entity) =>
        _context.Set<T>().Add(entity);
    

    public void Delete(T entity) =>
        _context.Set<T>().Remove(entity);

    public void DeleteRange(List<T> entities) =>
        _context.Set<T>().RemoveRange(entities);

    public virtual async Task<T?> GetByIdAsync(int tKey) =>
        await _context.Set<T>().FindAsync(tKey);
    

    public virtual async Task<IReadOnlyList<T>> GetAsync()
        => await _context.Set<T>().AsNoTracking().ToListAsync();
    

    public void Update(T entity) =>
        _context.Set<T>().Update(entity);

    public virtual async Task<T?> GetSingle(Expression<Func<T, bool>> predicate)
        => await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);

    public virtual async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        => await _context.Set<T>().Where(predicate).AsNoTracking().ToListAsync();
    
    
}