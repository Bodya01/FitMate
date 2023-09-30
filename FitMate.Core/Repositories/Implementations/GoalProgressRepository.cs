using FitMate.Core.Context;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitMate.Core.Repositories.Implementations
{
    public sealed class GoalProgressRepository : IGoalProgressRepository
    {
        private readonly FitMateContext _context;

        public GoalProgressRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(GoalProgress entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(GoalProgress entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(GoalProgress entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<GoalProgress> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.GoalProgressRecords.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<GoalProgress> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.GoalProgressRecords.Where(x => ids.Contains(x.Id));

        public IQueryable<GoalProgress> Get(Expression<Func<GoalProgress, bool>> expression, Expression<Func<GoalProgress, GoalProgress>> selector) =>
            _context.GoalProgressRecords.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(GoalProgress entity, Expression<Func<GoalProgress, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.GoalProgressRecords.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(GoalProgress entity, Expression<Func<GoalProgress, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.GoalProgressRecords.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}