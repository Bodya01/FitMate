using FitMate.Infrastructure.Models.Food;
using FitMate.Infrastucture.Dtos;

namespace FitMate.Business.Interfaces
{
    public interface IFoodService
    {
        Task<IEnumerable<FoodDto>> GetAllFoodsAsync(CancellationToken cancellationToken);
        Task<FoodDto> GetFoodByIdAsync(Guid id, CancellationToken cancellationToken);
        Task CreateFoodAsync(CreateFoodModel model, CancellationToken cancellationToken = default);
        Task UpdateFoodAsync(UpdateFoodModel model, CancellationToken cancellationToken = default);
        Task DeleteFoodAsync(Guid id, CancellationToken cancellationToken = default);
    }
}