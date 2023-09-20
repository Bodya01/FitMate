using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Core.Repositories.Implementations
{
    public class BodyweightTargetRepository : IBodyweightTargetRepository
    {
        private readonly FitMateContext _context;
        public BodyweightTargetRepository(FitMateContext context)
        {
            _context = context;
        }

        public async Task<BodyweightTarget> GetForUserAsync(string userId) =>
            await _context.BodyweightTargets.FirstOrDefaultAsync(target => target.UserId == userId);

        public async Task AddAsync(BodyweightTarget target)
        {
            await _context.BodyweightTargets.AddAsync(target);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BodyweightTarget target)
        {
            _context.BodyweightTargets.Update(target);
            await _context.SaveChangesAsync();
        }
    }
}
