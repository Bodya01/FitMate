using FitMate.Core.Context;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitMate.Core.Repositories.Implementations
{
    public sealed class GoalRepository : IGoalRepository
    {
        private readonly FitMateContext _context;

        public GoalRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Goal entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(Goal entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(Goal entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<Goal> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.Goals.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<Goal> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.Goals.Where(x => ids.Contains(x.Id));

        public IQueryable<Goal> Get(Expression<Func<Goal, bool>> expression, Expression<Func<Goal, Goal>> selector) =>
            _context.Goals.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(Goal entity, Expression<Func<Goal, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.Goals.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(Goal entity, Expression<Func<Goal, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.Goals.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}