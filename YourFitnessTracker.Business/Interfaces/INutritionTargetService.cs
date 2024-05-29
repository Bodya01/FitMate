using YourFitnessTracker.Infrastructure.Models.NutritionTarget;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface INutritionTargetService
    {
        Task<NutritionTargetDto> GetUserTargetAsync(string userId, CancellationToken cancellationToken = default);
        Task SetUserTargetAsync(NutritionTargetCalculationParameters calculationParameters, string userId, CancellationToken cancellationToken = default);
    }
}