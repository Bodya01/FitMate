using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(string userId, CancellationToken cancellationToken = default);
        Task UpdateUserHeight(string userId, int height, CancellationToken cancellationToken = default);
    }
}