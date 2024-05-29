using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace YourFitnessTracker.Core.Repositories.Implementations
{
    internal sealed class BodyweightTargetRepository : IBodyweightTargetRepository
    {
        private readonly YourFitnessTrackerContext _context;

        public BodyweightTargetRepository(YourFitnessTrackerContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(BodyweightTarget entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(BodyweightTarget entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(BodyweightTarget entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<BodyweightTarget> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.BodyweightTargets.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<BodyweightTarget> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.BodyweightTargets.Where(x => ids.Contains(x.Id));

        public IQueryable<BodyweightTarget> Get(Expression<Func<BodyweightTarget, bool>> expression, Expression<Func<BodyweightTarget, BodyweightTarget>> selector) =>
            _context.BodyweightTargets.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(BodyweightTarget entity, Expression<Func<BodyweightTarget, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.BodyweightTargets.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(BodyweightTarget entity, Expression<Func<BodyweightTarget, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.BodyweightTargets.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}
