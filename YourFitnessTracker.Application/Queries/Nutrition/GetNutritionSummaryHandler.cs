using MediatR;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Exceptions.Bodyweight;
using YourFitnessTracker.Infrastructure.Extensions;
using YourFitnessTracker.Infrastucture.Dtos;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Application.Queries.Nutrition
{
    public record GetNutritionSummary(string UserId) : IRequest<GetNutritionSummaryResponse>;

    public record GetNutritionSummaryResponse(List<FoodRecordDto> Records, NutritionTargetDto Target, int Age, float? Height, float? Weight);

    internal sealed class GetNutritionSummaryHandler : IRequestHandler<GetNutritionSummary, GetNutritionSummaryResponse>
    {
        private readonly INutritionTargetService _targetService;
        private readonly IFoodRecordService _foodRecordService;
        private readonly IUserService _userService;
        private readonly IBodyweightRecordService _bodyweightRecordService;
        private readonly IBodyweightTargetService _bodyweightTargetService;

        public GetNutritionSummaryHandler(INutritionTargetService targetService, IFoodRecordService foodRecordService, IUserService userService, IBodyweightRecordService bodyweightRecordService, IBodyweightTargetService bodyweightTargetService)
        {
            _targetService = targetService;
            _foodRecordService = foodRecordService;
            _userService = userService;
            _bodyweightRecordService = bodyweightRecordService;
            _bodyweightTargetService = bodyweightTargetService;
        }

        async Task<GetNutritionSummaryResponse> IRequestHandler<GetNutritionSummary, GetNutritionSummaryResponse>.Handle(GetNutritionSummary request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.UserId, cancellationToken);
            var target = await _targetService.GetUserTargetAsync(request.UserId, cancellationToken);
            var records = await _foodRecordService.GetRecordsForLastDays(28, request.UserId, cancellationToken);
            var lastBodyweightRecord = await _bodyweightRecordService.GetLastRecordAsync(request.UserId, cancellationToken);
            BodyweightTargetDto bodyweightTarget;
            try
            {
                bodyweightTarget = await _bodyweightTargetService.GetCurrentTargetAsync(request.UserId, cancellationToken);
            }
            catch (BodyweightTargetNotFoundException ex)
            {
                bodyweightTarget = new(default, default, default, default);
            }

            return new GetNutritionSummaryResponse(records.ToList(), target, user.DateOfBirth.GetAge(), user.Height, bodyweightTarget.TargetWeight != default ? bodyweightTarget.TargetWeight : lastBodyweightRecord?.Weight);
        }
    }
}