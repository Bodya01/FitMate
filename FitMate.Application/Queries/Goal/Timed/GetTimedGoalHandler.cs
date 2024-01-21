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
        private readonly ILogger<GetTimedGoalHandler> _logger;

        public GetTimedGoalHandler(ITimedGoalService timedGoalService, ILogger<GetTimedGoalHandler> logger)
        {
            _timedGoalService = timedGoalService;
            _logger = logger;
        }

        async Task<TimedGoalDto> IRequestHandler<GetTimedGoal, TimedGoalDto>.Handle(GetTimedGoal request, CancellationToken cancellationToken)
        {
            try
            {
                await _timedGoalService.GetTimedGoal(request.Id, request.UserId, cancellationToken);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync();
            }
            return await _timedGoalService.GetTimedGoal(request.Id, request.UserId, cancellationToken);
        }
    }
}