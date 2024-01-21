using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastucture.Dtos.Goals;
using MediatR;

namespace FitMate.Application.Queries.Goal
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
            var weightliftingGoals = await GetGoals(_weightliftingGoalService.GetWeightliftingGoalsForUser, request.UserId, cancellationToken);
            var timedGoals = await GetGoals(_timedGoalService.GetTimedGoalsForUser, request.UserId, cancellationToken);

            return (weightliftingGoals, timedGoals);
        }

        private static async Task<IEnumerable<TGoalDto>> GetGoals<TGoalDto>(Func<string, CancellationToken, Task<IEnumerable<TGoalDto>>> getGoalsFunc, string userId, CancellationToken cancellationToken)
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