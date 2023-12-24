using FitMate.Core.Context;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitMate.Core.Repositories.Implementations
{
    internal sealed class BodyweightRecordRepository : IBodyweightRecordRepository
    {
        private readonly FitMateContext _context;

        public BodyweightRecordRepository(FitMateContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(BodyweightRecord entity, CancellationToken cancellationToken = default) =>
            await _context.AddAsync(entity, cancellationToken);

        public async Task CreateRangeAsync(IEnumerable<BodyweightRecord> bodyweightRecords, CancellationToken cancellationToken = default) =>
            await _context.AddRangeAsync(bodyweightRecords, cancellationToken);

        public async Task UpdateAsync(BodyweightRecord entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Update(entity), cancellationToken);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity is not null) await Task.Run(() => _context.Remove(entity), cancellationToken);
        }

        public async Task DeleteAsync(BodyweightRecord entity, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.Remove(entity), cancellationToken);

        public async Task<BodyweightRecord> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _context.BodyweightRecords.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        public IQueryable<BodyweightRecord> GetEntitiesAsync(IEnumerable<Guid> ids) =>
            _context.BodyweightRecords.Where(x => ids.Contains(x.Id));

        public IQueryable<BodyweightRecord> Get(Expression<Func<BodyweightRecord, bool>> expression, Expression<Func<BodyweightRecord, BodyweightRecord>> selector) =>
            _context.BodyweightRecords.Select(selector).Where(expression);

        public async Task LoadNavigationPropertyExplicitly<TProperty>(BodyweightRecord entity, Expression<Func<BodyweightRecord, TProperty>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.BodyweightRecords.Entry(entity).Reference(relation).LoadAsync(cancellationToken);

        public async Task LoadNavigationCollectionExplicitly<TProperty>(BodyweightRecord entity, Expression<Func<BodyweightRecord, IEnumerable<TProperty>>> relation, CancellationToken cancellationToken = default) where TProperty : class =>
            await _context.BodyweightRecords.Entry(entity).Collection(relation).LoadAsync(cancellationToken);

        public async Task DeleteRangeAsync(IEnumerable<BodyweightRecord> bodyweightRecords, CancellationToken cancellationToken = default) =>
            await Task.Run(() => _context.RemoveRange(bodyweightRecords), cancellationToken);
    }
}