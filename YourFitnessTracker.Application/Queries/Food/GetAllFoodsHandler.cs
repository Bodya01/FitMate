using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Application.Queries.Food
{
    public record GetAllFoods() : IRequest<IEnumerable<FoodDto>>;

    internal sealed class GetAllFoodsHandler : FitMateRequestHandler<GetAllFoods, IEnumerable<FoodDto>>
    {
        private readonly IFoodService _foodService;

        public GetAllFoodsHandler(IFoodService foodService)
        {
            _foodService = foodService;
        }

        public override async Task<IEnumerable<FoodDto>> Handle(GetAllFoods request, CancellationToken cancellationToken) =>
            await _foodService.GetAllFoodsAsync(cancellationToken);
    }
}