using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace YourFitnessTracker.Core.Repositories.Implementations
{
    internal sealed class WeightliftingProgressRepository : IWeightliftingProgressRepository
    {
        private readonly YourFitnessTrackerContext _context;

        public WeightliftingProgressRepository(YourFitnessTrackerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(WeightliftingProgress entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(WeightliftingProgress entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(WeightliftingProgress entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<WeightliftingProgress> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.WeightliftingProgressRecords.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<WeightliftingProgress> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.WeightliftingProgressRecords.Where(x => ids.Contains(x.Id));

        public IQueryable<WeightliftingProgress> Get(Expression<Func<WeightliftingProgress, bool>> expression, Expression<Func<WeightliftingProgress, WeightliftingProgress>> selector) =>
            _context.WeightliftingProgressRecords.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(WeightliftingProgress entity, Expression<Func<WeightliftingProgress, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.WeightliftingProgressRecords.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(WeightliftingProgress entity, Expression<Func<WeightliftingProgress, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.WeightliftingProgressRecords.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}
