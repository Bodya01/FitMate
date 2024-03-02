using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Application.Queries.Nutrition
{
    public record GetNutritionSummary(string UserId) : IRequest<GetNutritionSummaryResponse>;

    public record GetNutritionSummaryResponse(List<FoodRecordDto> Records, NutritionTargetDto Target);

    internal sealed class GetNutritionSummaryHandler : IRequestHandler<GetNutritionSummary, GetNutritionSummaryResponse>
    {
        private readonly INutritionTargetService _targetService;
        private readonly IFoodRecordService _foodRecordService;

        public GetNutritionSummaryHandler(INutritionTargetService targetService, IFoodRecordService foodRecordService)
        {
            _targetService = targetService;
            _foodRecordService = foodRecordService;
        }

        async Task<GetNutritionSummaryResponse> IRequestHandler<GetNutritionSummary, GetNutritionSummaryResponse>.Handle(GetNutritionSummary request, CancellationToken cancellationToken)
        {
            var target = await _targetService.GetUserTargetAsync(request.UserId, cancellationToken);
            var records = await _foodRecordService.GetRecordsForLastDays(28, request.UserId, cancellationToken);

            return new GetNutritionSummaryResponse(records.ToList(), target);
        }
    }
}