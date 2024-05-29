using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace YourFitnessTracker.Core.Repositories.Implementations
{
    internal sealed class TimedProgressRepository : ITimedProgressRepository
    {
        private readonly YourFitnessTrackerContext _context;

        public TimedProgressRepository(YourFitnessTrackerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TimedProgress entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(TimedProgress entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(TimedProgress entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<TimedProgress> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.TimedProgressRecords.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<TimedProgress> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.TimedProgressRecords.Where(x => ids.Contains(x.Id));

        public IQueryable<TimedProgress> Get(Expression<Func<TimedProgress, bool>> expression, Expression<Func<TimedProgress, TimedProgress>> selector) =>
            _context.TimedProgressRecords.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(TimedProgress entity, Expression<Func<TimedProgress, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.TimedProgressRecords.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(TimedProgress entity, Expression<Func<TimedProgress, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.TimedProgressRecords.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}
