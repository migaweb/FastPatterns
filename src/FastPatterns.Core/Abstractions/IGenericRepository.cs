using FastPatterns.Core.Entities;
using System.Linq.Expressions;

namespace FastPatterns.Core.Abstractions;
/// <summary>
/// Represents a generic repository for performing CRUD operations on entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity, which must inherit from <see cref="BaseEntity"/>.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
  /// <summary>
  /// Retrieves an entity by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  /// <returns>The entity if found; otherwise, <c>null</c>.</returns>
  Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

  /// <summary>
  /// Retrieves a collection of entities based on the specified filter, order, and includes.
  /// </summary>
  /// <param name="filter">An optional filter expression to apply.</param>
  /// <param name="orderBy">An optional ordering function.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  /// <param name="includes">Optional related entities to include in the query.</param>
  /// <returns>A collection of entities matching the criteria.</returns>
  Task<IEnumerable<TEntity>> GetAsync(
      Expression<Func<TEntity, bool>>? filter = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
      CancellationToken cancellationToken = default,
      params Expression<Func<TEntity, object>>[] includes);

  /// <summary>
  /// Inserts a new entity into the repository.
  /// </summary>
  /// <param name="entity">The entity to insert.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

  /// <summary>
  /// Updates an existing entity in the repository.
  /// </summary>
  /// <param name="entity">The entity to update.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

  /// <summary>
  /// Deletes an entity from the repository by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity to delete.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  Task DeleteAsync(object id, CancellationToken cancellationToken = default);

  /// <summary>
  /// Deletes an entity from the repository.
  /// </summary>
  /// <param name="entity">The entity to delete.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
