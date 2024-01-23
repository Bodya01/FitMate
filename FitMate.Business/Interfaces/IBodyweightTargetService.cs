using FitMate.Infrastructure.Models.BodyweightTarget;

namespace FitMate.Business.Interfaces
{
    public interface IBodyweightTargetService
    {
        Task UpdateTargetAsync(UpdateBodyweightTargetModel model, CancellationToken cancellationToken = default);
    }
}