using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;
using MediatR;

namespace YourFitnessTracker.Application.Queries.Food
{
    public record GetAllFoods() : IRequest<IEnumerable<FoodDto>>;

    internal sealed class GetAllFoodsHandler : IRequestHandler<GetAllFoods, IEnumerable<FoodDto>>
    {
        private readonly IFoodService _foodService;

        public GetAllFoodsHandler(IFoodService foodService)
        {
            _foodService = foodService;
        }

        public async Task<IEnumerable<FoodDto>> Handle(GetAllFoods request, CancellationToken cancellationToken) =>
            await _foodService.GetAllFoodsAsync(cancellationToken);
    }
}