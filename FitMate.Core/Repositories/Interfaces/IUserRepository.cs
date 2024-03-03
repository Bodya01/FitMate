using FitMate.Infrastructure.Entities;

namespace FitMate.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task UpdateAsync(FitnessUser user, CancellationToken cancellationToken = default);
    }
}