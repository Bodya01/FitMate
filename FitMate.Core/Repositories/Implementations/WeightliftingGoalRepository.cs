using FitMate.Core.Context;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitMate.Core.Repositories.Implementations
{
    // TODO: Review goal repositories and DAL overall
    internal sealed class WeightliftingGoalRepository : IWeightliftingGoalRepository
    {
        private readonly FitMateContext _context;

        public WeightliftingGoalRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(WeightliftingGoal entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(WeightliftingGoal entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(WeightliftingGoal entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<WeightliftingGoal> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.WeightliftingGoals.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<WeightliftingGoal> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.WeightliftingGoals.Where(x => ids.Contains(x.Id));

        public IQueryable<WeightliftingGoal> Get(Expression<Func<WeightliftingGoal, bool>> expression, Expression<Func<WeightliftingGoal, WeightliftingGoal>> selector) =>
            _context.WeightliftingGoals.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(WeightliftingGoal entity, Expression<Func<WeightliftingGoal, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.WeightliftingGoals.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(WeightliftingGoal entity, Expression<Func<WeightliftingGoal, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.WeightliftingGoals.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}