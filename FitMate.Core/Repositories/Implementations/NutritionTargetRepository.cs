using FitMate.Core.Context;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Core.Repositories.Implementations
{
    public class NutritionTargetRepository : INutritionTargetRepository
    {
        private readonly FitMateContext _context;

        public NutritionTargetRepository(FitMateContext context)
        {
            _context = context;
        }
        public async Task<NutritionTarget> GetNutritionTargetForUser(string userId) =>
            await _context.NutritionTargets.FirstOrDefaultAsync(n => n.UserId == userId);
    }
}