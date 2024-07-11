global using System.Collections;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace BlogPost.Persistence.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlogPostContext _context;
    private Hashtable _repositories;

    public UnitOfWork(BlogPostContext context)
    {
        _context = context;
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        if (_repositories == null) _repositories = new Hashtable();

        var type = typeof(T).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)),
                _context);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }


    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var save = await _context.SaveChangesAsync();
            return save;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}