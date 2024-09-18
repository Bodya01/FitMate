using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Application.Queries.Food
{
    public record GetAddFood(DateTime ConsumptionDate) : IRequest<GetAddFoodResponse>
    {
        public string UserId { get; set; }
    }

    public record GetAddFoodResponse(IEnumerable<FoodDto> Foods, IEnumerable<FoodRecordDto> FoodRecords);

    internal sealed class GetAddFoodHandler : FitMateRequestHandler<GetAddFood, GetAddFoodResponse>
    {
        private readonly IFoodService _foodService;
        private readonly IFoodRecordService _foodRecordService;

        public GetAddFoodHandler(IFoodService foodService, IFoodRecordService foodRecordService)
        {
            _foodService = foodService;
            _foodRecordService = foodRecordService;
        }

        public override async Task<GetAddFoodResponse> Handle(GetAddFood request, CancellationToken cancellationToken)
        {
            var foods = await _foodService.GetAllFoodsAsync(cancellationToken);
            var foodRecords = await _foodRecordService.GetRecordsByDate(request.ConsumptionDate, request.UserId, cancellationToken);

            return new GetAddFoodResponse(foods, foodRecords);
        }
    }
}
