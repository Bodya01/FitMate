using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastucture.Dtos.Goals;

namespace YourFitnessTracker.Application.Queries.Goal
{
    public record GoalSummaryQuery(string UserId) : IRequest<(IEnumerable<WeightliftingGoalDto>, IEnumerable<TimedGoalDto>)>;

    internal sealed class GoalSummaryQueryHandler : FitMateRequestHandler<GoalSummaryQuery, (IEnumerable<WeightliftingGoalDto>, IEnumerable<TimedGoalDto>)>
    {
        private readonly IWeightliftingGoalService _weightliftingGoalService;
        private readonly ITimedGoalService _timedGoalService;

        public GoalSummaryQueryHandler(IWeightliftingGoalService weightliftingGoalService, ITimedGoalService timedGoalService)
        {
            _weightliftingGoalService = weightliftingGoalService;
            _timedGoalService = timedGoalService;
        }

        public override async Task<(IEnumerable<WeightliftingGoalDto>, IEnumerable<TimedGoalDto>)> Handle(GoalSummaryQuery request, CancellationToken cancellationToken)
        {
            //Can't use Task.WhenAll due to UoW. Easy to bypass if performance boost needed
            var weightliftingGoals = await TryGetCollectionAsync(_weightliftingGoalService.GetWeightliftingGoalsForUser(request.UserId, cancellationToken));
            var timedGoals = await TryGetCollectionAsync(_timedGoalService.GetTimedGoalsForUser(request.UserId, cancellationToken));

            return (weightliftingGoals, timedGoals);
        }
    }
}