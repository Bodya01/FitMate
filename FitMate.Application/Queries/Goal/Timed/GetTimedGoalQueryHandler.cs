using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos.Goals;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.Goal.Timed
{
    public record GetTimedGoalQuery(Guid Id) : IRequest<TimedGoalDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetTimedGoalQueryHandler : IRequestHandler<GetTimedGoalQuery, TimedGoalDto>
    {
        private readonly ITimedGoalService _timedGoalService;
        private readonly ILogger<GetTimedGoalQueryHandler> _logger;

        public GetTimedGoalQueryHandler(ITimedGoalService timedGoalService, ILogger<GetTimedGoalQueryHandler> logger)
        {
            _timedGoalService = timedGoalService;
            _logger = logger;
        }

        async Task<TimedGoalDto> IRequestHandler<GetTimedGoalQuery, TimedGoalDto>.Handle(GetTimedGoalQuery request, CancellationToken cancellationToken) =>
            await _timedGoalService.GetTimedGoal(request.Id, request.UserId, cancellationToken);
    }
}