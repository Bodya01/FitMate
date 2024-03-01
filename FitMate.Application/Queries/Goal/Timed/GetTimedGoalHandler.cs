using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos.Goals;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.Goal.Timed
{
    public record GetTimedGoal(Guid Id) : IRequest<TimedGoalDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetTimedGoalHandler : IRequestHandler<GetTimedGoal, TimedGoalDto>
    {
        private readonly ITimedGoalService _timedGoalService;

        public GetTimedGoalHandler(ITimedGoalService timedGoalService)
        {
            _timedGoalService = timedGoalService;
        }

        async Task<TimedGoalDto> IRequestHandler<GetTimedGoal, TimedGoalDto>.Handle(GetTimedGoal request, CancellationToken cancellationToken) =>
            await _timedGoalService.GetTimedGoal(request.Id, request.UserId, cancellationToken);
    }
}