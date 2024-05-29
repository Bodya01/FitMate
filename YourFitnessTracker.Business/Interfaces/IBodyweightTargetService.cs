using YourFitnessTracker.Infrastructure.Models.BodyweightTarget;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface IBodyweightTargetService
    {
        Task<BodyweightTargetDto> GetCurrentTargetAsync(string userId, CancellationToken cancellationToken);
        Task UpdateTargetAsync(UpdateBodyweightTargetModel model, CancellationToken cancellationToken = default);
    }
}