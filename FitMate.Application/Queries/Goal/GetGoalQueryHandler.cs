using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos.Goals;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.Goal
{
    public record GetGoalQuery(Guid Id, string UserId) : IRequest<GoalDto>;

    internal sealed class GetGoalQueryHandler : IRequestHandler<GetGoalQuery, GoalDto>
    {
        private readonly ILogger<GetGoalQueryHandler> _logger;
        private readonly IGoalService _goalService;

        public GetGoalQueryHandler(ILogger<GetGoalQueryHandler> logger, IGoalService goalService)
        {
            _logger = logger;
            _goalService = goalService;
        }

        public async Task<GoalDto> Handle(GetGoalQuery request, CancellationToken cancellationToken) =>
            await _goalService.GetGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}