using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos.Goals;

namespace YourFitnessTracker.Application.Queries.Goal.Timed
{
    public record GetTimedGoal(Guid Id) : IRequest<TimedGoalDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetTimedGoalHandler : FitMateRequestHandler<GetTimedGoal, TimedGoalDto>
    {
        private readonly ITimedGoalService _timedGoalService;

        public GetTimedGoalHandler(ITimedGoalService timedGoalService)
        {
            _timedGoalService = timedGoalService;
        }

        public override async Task<TimedGoalDto> Handle(GetTimedGoal request, CancellationToken cancellationToken) =>
            await _timedGoalService.GetTimedGoal(request.Id, request.UserId, cancellationToken);
    }
}