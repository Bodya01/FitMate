using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitMate.Core.Repositories.Implementations
{
    public sealed class FoodRecordRepository : IFoodRecordRepository
    {
        private readonly FitMateContext _context;

        public FoodRecordRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(FoodRecord entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task CreateRangeAsync(IEnumerable<FoodRecord> entities, CancellationToken cancellationToken = default) =>
            await _context.AddRangeAsync(entities, cancellationToken);

        public async Task UpdateAsync(FoodRecord entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(FoodRecord entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task DeleteRangeAsync(IEnumerable<FoodRecord> records, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.RemoveRange(records), cancellationToken);

        public async Task<FoodRecord> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.FoodRecords.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<FoodRecord> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.FoodRecords.Where(x => ids.Contains(x.Id));

        public IQueryable<FoodRecord> Get(Expression<Func<FoodRecord, bool>> expression, Expression<Func<FoodRecord, FoodRecord>> selector) =>
            _context.FoodRecords.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(FoodRecord entity, Expression<Func<FoodRecord, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.FoodRecords.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(FoodRecord entity, Expression<Func<FoodRecord, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.FoodRecords.Entry(entity).Collection(relation).LoadAsync(cancellationToken);
    }
}