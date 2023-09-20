using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Core.Repositories.Implementations
{
    public class BodyweightRepository : IBodyweightRepository
    {
        private FitMateContext _context;

        public BodyweightRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task StoreBodyweightRecord(BodyweightRecord record)
        {
            _context.BodyweightRecords.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task StoreBodyweightRecords(List<BodyweightRecord> records)
        {
            await _context.BodyweightRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();
        }

        public async Task<BodyweightRecord[]> GetBodyweightRecords(string userId, bool ascendingOrder = false)
        {
            if (!ascendingOrder)
            {
                return await _context.BodyweightRecords
                    .Where(record => record.UserId == userId)
                    .OrderByDescending(record => record.Date)
                    .ToArrayAsync();
            }
            else
            {
                return await _context.BodyweightRecords
                    .Where(record => record.UserId == userId)
                    .OrderBy(record => record.Date)
                    .ToArrayAsync();
            }
        }

        public async Task<BodyweightTarget> GetBodyweightTarget(string userId) =>
            await _context.BodyweightTargets.FirstOrDefaultAsync(target => target.UserId == userId);

        public async Task DeleteExistingRecords(string userId)
        {
            var existingRecords = await _context.BodyweightRecords.Where(record => record.UserId == userId).ToListAsync();
            _context.BodyweightRecords.RemoveRange(existingRecords);

            await _context.SaveChangesAsync();
        }

        public async Task StoreBodyweightTarget(BodyweightTarget target)
        {
            if (target.Id == 0)
            {
                await _context.BodyweightTargets.AddAsync(target);
            }
            else
            {
                _context.BodyweightTargets.Update(target);
            }

            await _context.SaveChangesAsync();
        }
    }
}