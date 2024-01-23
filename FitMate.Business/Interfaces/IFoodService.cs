using FitMate.Infrastructure.Models.Food;

namespace FitMate.Business.Interfaces
{
    public interface IFoodService
    {
        Task CreateFoodAsync(CreateFoodModel model, CancellationToken cancellationToken = default);
        Task UpdateFoodAsync(UpdateFoodModel model, CancellationToken cancellationToken = default);
    }
}