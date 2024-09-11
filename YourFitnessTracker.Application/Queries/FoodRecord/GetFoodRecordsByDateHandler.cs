using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Application.Queries.FoodRecord
{
    public record GetFoodRecordsByDate(string UserId, DateTime ConsumptionDate) : IRequest<List<FoodRecordDto>>;

    internal sealed class GetFoodRecordsByDateHanlder : FitMateRequestHandler<GetFoodRecordsByDate, List<FoodRecordDto>>
    {
        private readonly IFoodRecordService _foodRecordService;

        public GetFoodRecordsByDateHanlder(IFoodRecordService foodRecordService)
        {
            _foodRecordService = foodRecordService;
        }

        public override async Task<List<FoodRecordDto>> Handle(GetFoodRecordsByDate request, CancellationToken cancellationToken) =>
            (await _foodRecordService.GetRecordsByDate(request.ConsumptionDate, request.UserId, cancellationToken)).ToList();
    }
}