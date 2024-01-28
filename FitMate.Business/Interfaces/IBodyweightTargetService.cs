using FitMate.Infrastructure.Models.BodyweightTarget;
using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Business.Interfaces
{
    public interface IBodyweightTargetService
    {
        Task<BodyweightTargetDto> GetCurrentTargetAsync(string userId, CancellationToken cancellationToken);
        Task UpdateTargetAsync(UpdateBodyweightTargetModel model, CancellationToken cancellationToken = default);
    }
}