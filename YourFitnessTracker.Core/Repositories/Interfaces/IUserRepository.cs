using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task UpdateAsync(FitnessUser user, CancellationToken cancellationToken = default);
    }
}