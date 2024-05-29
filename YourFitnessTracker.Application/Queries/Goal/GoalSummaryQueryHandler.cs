using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastucture.Dtos.Goals;
using MediatR;

namespace YourFitnessTracker.Application.Queries.Goal
{
    public record GoalSummaryQuery(string UserId) : IRequest<(IEnumerable<WeightliftingGoalDto>, IEnumerable<TimedGoalDto>)>;

    internal sealed class GoalSummaryQueryHandler : IRequestHandler<GoalSummaryQuery, (IEnumerable<WeightliftingGoalDto>, IEnumerable<TimedGoalDto>)>
    {
        private readonly IWeightliftingGoalService _weightliftingGoalService;
        private readonly ITimedGoalService _timedGoalService;

        public GoalSummaryQueryHandler(IWeightliftingGoalService weightliftingGoalService, ITimedGoalService timedGoalService)
        {
            _weightliftingGoalService = weightliftingGoalService;
            _timedGoalService = timedGoalService;
        }

        async Task<(IEnumerable<WeightliftingGoalDto>, IEnumerable<TimedGoalDto>)> IRequestHandler<GoalSummaryQuery, (IEnumerable<WeightliftingGoalDto>, IEnumerable<TimedGoalDto>)>.Handle(GoalSummaryQuery request, CancellationToken cancellationToken)
        {
            //Can't use Task.WhenAll due to UoW. Easy to bypass if performance boost needed
            var weightliftingGoals = await GetGoalsAsync(_weightliftingGoalService.GetWeightliftingGoalsForUser, request.UserId, cancellationToken);
            var timedGoals = await GetGoalsAsync(_timedGoalService.GetTimedGoalsForUser, request.UserId, cancellationToken);

            return (weightliftingGoals, timedGoals);
        }

        private static async Task<IEnumerable<TGoalDto>> GetGoalsAsync<TGoalDto>(Func<string, CancellationToken, Task<IEnumerable<TGoalDto>>> getGoalsFunc, string userId, CancellationToken cancellationToken)
            where TGoalDto : GoalDto
        {
            try
            {
                return await getGoalsFunc(userId, cancellationToken);
            }
            catch (EntityNotFoundException)
            {
                return Enumerable.Empty<TGoalDto>();
            }
        }
    }
}