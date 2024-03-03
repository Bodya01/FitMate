using FitMate.Infrastructure.Entities;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(string userId, CancellationToken cancellationToken = default);
        Task UpdateUserHeight(string userId, int height, CancellationToken cancellationToken = default);
    }
}