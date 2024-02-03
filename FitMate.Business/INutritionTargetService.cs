using FitMate.Infrastucture.Dtos;

namespace FitMate.Business
{
    public interface INutritionTargetService
    {
        Task<NutritionTargetDto> GetTargetForUser(string userId, CancellationToken cancellationToken = default);
    }
}