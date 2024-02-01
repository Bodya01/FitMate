using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Application.Queries.FoodRecord
{
    public record GetFoodRecords(string UserId, uint PreviousDays) : IRequest<List<FoodRecordDto>>;

    internal sealed class GetFoodRecordsHanlder : IRequestHandler<GetFoodRecords, List<FoodRecordDto>>
    {
        private readonly IFoodRecordService _foodRecordService;

        public GetFoodRecordsHanlder(IFoodRecordService foodRecordService)
        {
            _foodRecordService = foodRecordService;
        }

        public async Task<List<FoodRecordDto>> Handle(GetFoodRecords request, CancellationToken cancellationToken) =>
            (await _foodRecordService.GetRecordsForLastDays(request.PreviousDays, request.UserId, cancellationToken)).ToList();
    }
}