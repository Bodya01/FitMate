using FitMate.Business.Interfaces.Base;
using FitMate.Infrastructure.Models.GoalProgress.Timed;

namespace FitMate.Business.Interfaces
{
    public interface ITimedProgressService
    {
        Task CreateTimedProgressAsync(CreateTimedProgressModel model, CancellationToken cancellationToken = default);
    }
}