using YourFitnessTracker.Infrastructure.Models.GoalProgress.Weightlifting;
using YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface IWeightliftingProgressService
    {
        Task<IEnumerable<WeightliftingProgressDto>> GetRecordsForGoalAsync(Guid id, string userId, CancellationToken cancellationToken = default);
        Task CreateWeightliftingProgressAsync(CreateWeightliftingProgressModel model, CancellationToken cancellationToken = default);
    }
}