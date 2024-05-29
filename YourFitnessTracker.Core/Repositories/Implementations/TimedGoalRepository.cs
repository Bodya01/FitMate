using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core.Repositories.Implementations
{
    internal sealed class TimedGoalRepository : ITimedGoalRepository
    {
        private readonly YourFitnessTrackerContext _context;

        public TimedGoalRepository(YourFitnessTrackerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TimedGoal entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(TimedGoal entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(TimedGoal entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<TimedGoal> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.TimedGoals.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<TimedGoal> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.TimedGoals.Where(x => ids.Contains(x.Id));

        public IQueryable<TimedGoal> Get(Expression<Func<TimedGoal, bool>> expression, Expression<Func<TimedGoal, TimedGoal>> selector) =>
            _context.TimedGoals.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(TimedGoal entity, Expression<Func<TimedGoal, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.TimedGoals.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(TimedGoal entity, Expression<Func<TimedGoal, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.TimedGoals.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}