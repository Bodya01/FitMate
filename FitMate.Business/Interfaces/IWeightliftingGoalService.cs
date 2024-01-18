using FitMate.Infrastucture.Dtos.Goals;

namespace FitMate.Business.Interfaces
{
    public interface IWeightliftingGoalService
    {
        Task<IEnumerable<WeightliftingGoalDto>> GetWeightliftingGoalsForUser(string  userId, CancellationToken cancellationToken = default);
    }
}