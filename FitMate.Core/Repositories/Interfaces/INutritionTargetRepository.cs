using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface INutritionTargetRepository
    {
        Task<NutritionTarget> GetTargetForUserAsync(string userId, CancellationToken cancellationToken = default);
    }
}