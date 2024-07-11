namespace BlogPost.Application.Contract.Interface;

/// <summary>
/// IUnitOfWork
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    /// <summary>
    /// Saves changes to database, previously opening a transaction
    /// only when none exists. The transaction is opened with isolation
    /// level set in Unit of Work before calling this method.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
}