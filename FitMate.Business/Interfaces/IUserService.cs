using FitMate.Infrastructure.Entities;

namespace FitMate.Business.Interfaces
{
    public interface IUserService
    {
        Task<FitnessUser?> GetUserAsync(CancellationToken cancellationToken = default);
        Task<string> GetUserIdAsync(CancellationToken cancellationToken = default);
    }
}