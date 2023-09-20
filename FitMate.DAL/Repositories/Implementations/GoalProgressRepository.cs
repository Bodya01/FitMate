using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Core.Repositories.Implementations
{
    public class GoalProgressRepository : IGoalProgressRepository
    {
        private readonly FitMateContext _context;

        public GoalProgressRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task<List<GoalProgress>> GetForUserAsync(string userId, Guid goalId, bool ascendingOrder = false)
        {
            var query = _context.GoalProgressRecords.Where(r => r.GoalId == goalId && r.UserId == userId);

            query = ascendingOrder ? query.OrderBy(r => r.Date) : query.OrderByDescending(r => r.Date);

            var result = await query.ToListAsync();

            return result;
        }

        public async Task AddAsync(GoalProgress progress)
        {
            _context.GoalProgressRecords.Add(progress);
            await _context.SaveChangesAsync();
        }
    }
}