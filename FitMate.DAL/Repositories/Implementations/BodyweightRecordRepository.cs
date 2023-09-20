using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Core.Repositories.Implementations
{
    public class BodyweightRecordRepository : IBodyweightRecordRepository
    {
        private FitMateContext _context;

        public BodyweightRecordRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BodyweightRecord record)
        {
            _context.BodyweightRecords.Add(record);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<BodyweightRecord> records)
        {
            await _context.BodyweightRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();
        }

        public async Task<BodyweightRecord[]> GetAllForUserAsync(string userId, bool ascendingOrder = false)
        {
            var query = _context.BodyweightRecords
                        .Where(record => record.UserId == userId);

            query = ascendingOrder ? query.OrderBy(record => record.Date) : query.OrderByDescending(record => record.Date);

            return await query.ToArrayAsync();
        }


        public async Task DeleteAllForUser(string userId)
        {
            var existingRecords = await _context.BodyweightRecords.Where(record => record.UserId == userId).ToListAsync();
            _context.BodyweightRecords.RemoveRange(existingRecords);

            await _context.SaveChangesAsync();
        }
    }
}