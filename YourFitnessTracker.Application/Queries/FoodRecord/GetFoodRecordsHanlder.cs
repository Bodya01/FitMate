using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Application.Queries.FoodRecord
{
    public record GetFoodRecords(string UserId, uint PreviousDays) : IRequest<List<FoodRecordDto>>;

    internal sealed class GetFoodRecordsHanlder : FitMateRequestHandler<GetFoodRecords, List<FoodRecordDto>>
    {
        private readonly IFoodRecordService _foodRecordService;

        public GetFoodRecordsHanlder(IFoodRecordService foodRecordService)
        {
            _foodRecordService = foodRecordService;
        }

        public override async Task<List<FoodRecordDto>> Handle(GetFoodRecords request, CancellationToken cancellationToken) =>
            (await _foodRecordService.GetRecordsForLastDays(request.PreviousDays, request.UserId, cancellationToken)).ToList();
    }
}