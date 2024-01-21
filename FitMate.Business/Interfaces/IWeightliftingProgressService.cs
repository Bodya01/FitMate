using FitMate.Infrastructure.Models.GoalProgress.Weightlifting;

namespace FitMate.Business.Interfaces
{
    public interface IWeightliftingProgressService
    {
        Task CreateWeightliftingProgressAsync(CreateWeightliftingProgressModel model, CancellationToken cancellationToken = default);
    }
}