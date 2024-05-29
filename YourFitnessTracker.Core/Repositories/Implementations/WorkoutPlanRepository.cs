using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core.Repositories.Implementations
{
    internal sealed class WorkoutPlanRepository : IWorkoutPlanRepository
    {
        private readonly YourFitnessTrackerContext _context;

        public WorkoutPlanRepository(YourFitnessTrackerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(WorkoutPlan entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(WorkoutPlan entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(WorkoutPlan entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<WorkoutPlan> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.WorkoutPlans.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<WorkoutPlan> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.WorkoutPlans.Where(x => ids.Contains(x.Id));

        public IQueryable<WorkoutPlan> Get(Expression<Func<WorkoutPlan, bool>> expression, Expression<Func<WorkoutPlan, WorkoutPlan>> selector) =>
            _context.WorkoutPlans.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(WorkoutPlan entity, Expression<Func<WorkoutPlan, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.WorkoutPlans.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(WorkoutPlan entity, Expression<Func<WorkoutPlan, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.WorkoutPlans.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}
