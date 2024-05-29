using System.Linq.Expressions;

namespace YourFitnessTracker.Core.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);
        IQueryable<T> GetEntitiesAsync(IEnumerable<Guid> ids);
        IQueryable<T> Get(Expression<Func<T, bool>> expression, Expression<Func<T, T>> selector);
        Task LoadNavigationPropertyExplicitly<TProperty>(T entity, Expression<Func<T, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class;
        Task LoadNavigationCollectionExplicitly<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class;
    }
}