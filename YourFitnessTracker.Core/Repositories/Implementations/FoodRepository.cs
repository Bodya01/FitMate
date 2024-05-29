using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core.Repositories.Implementations
{
    internal sealed class FoodRepository : IFoodRepository
    {
        private readonly YourFitnessTrackerContext _context;

        public FoodRepository(YourFitnessTrackerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Food entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(Food entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(Food entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<Food> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.Foods.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<Food> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.Foods.Where(x => ids.Contains(x.Id));

        public IQueryable<Food> Get(Expression<Func<Food, bool>> expression, Expression<Func<Food, Food>> selector) =>
            _context.Foods.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(Food entity, Expression<Func<Food, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.Foods.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(Food entity, Expression<Func<Food, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.Foods.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}
