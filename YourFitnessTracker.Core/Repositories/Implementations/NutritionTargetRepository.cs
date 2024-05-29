using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core.Repositories.Implementations
{
    internal sealed class NutritionTargetRepository : INutritionTargetRepository
    {
        private readonly YourFitnessTrackerContext _context;

        public NutritionTargetRepository(YourFitnessTrackerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(NutritionTarget entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(NutritionTarget entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(NutritionTarget entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<NutritionTarget> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.NutritionTargets.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<NutritionTarget> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.NutritionTargets.Where(x => ids.Contains(x.Id));

        public IQueryable<NutritionTarget> Get(Expression<Func<NutritionTarget, bool>> expression, Expression<Func<NutritionTarget, NutritionTarget>> selector) =>
            _context.NutritionTargets.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(NutritionTarget entity, Expression<Func<NutritionTarget, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.NutritionTargets.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(NutritionTarget entity, Expression<Func<NutritionTarget, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.NutritionTargets.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}