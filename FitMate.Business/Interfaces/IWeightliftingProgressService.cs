using FitMate.Infrastructure.Models.GoalProgress.Weightlifting;
using FitMate.Infrastucture.Dtos.GoalProgress;

namespace FitMate.Business.Interfaces
{
    public interface IWeightliftingProgressService
    {
        Task<IEnumerable<WeightliftingProgressDto>> GetRecordsForGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task CreateWeightliftingProgressAsync(CreateWeightliftingProgressModel model, CancellationToken cancellationToken = default);
    }
}