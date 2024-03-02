using FitMate.Infrastructure.Models.NutritionTarget;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Interfaces
{
    public interface INutritionTargetService
    {
        Task<NutritionTargetDto> GetUserTargetAsync(string userId, CancellationToken cancellationToken = default);
        Task SetUserTargetAsync(NutritionTargetCalculationParameters calculationParameters, string userId, CancellationToken cancellationToken = default);
    }
}