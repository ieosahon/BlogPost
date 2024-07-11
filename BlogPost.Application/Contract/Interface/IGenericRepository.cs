using System.Linq.Expressions;

namespace BlogPost.Application.Contract.Interface;

public interface IGenericRepository <T> where T : class
{
        /// <summary>
        /// Gets all query model asynchronously.
        /// </summary>
        /// <returns>The task representing the asynchronous operation, returning the query model.</returns>
        Task<IReadOnlyList<T>> GetAsync();

        /// <summary>
        /// Gets the query model by its ID asynchronously.
        /// </summary>
        /// <param name="tKey">The ID of the query model.</param>
        /// <returns>The task representing the asynchronous operation, returning the query model.</returns>
        Task<T?> GetByIdAsync(int tKey);
        

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Removes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Delete(T entity);
        
        /// <summary>
        /// Removes a bulk entity from the repository.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        void DeleteRange(List<T> entities);

        Task<T?> GetSingle(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);


}