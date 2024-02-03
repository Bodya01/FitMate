using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Interfaces
{
    public interface INutritionTargetService
    {
        Task<NutritionTargetDto> GetTargetForUser(string userId, CancellationToken cancellationToken = default);
    }
}