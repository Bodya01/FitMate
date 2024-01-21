using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos.GoalProgress;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.GoalProgress
{
    public record GetTimedProgressQuery(Guid Id) : IRequest<IEnumerable<TimedProgressDto>>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetTimedProgressQueryHandler : IRequestHandler<GetTimedProgressQuery, IEnumerable<TimedProgressDto>>
    {
        private readonly ILogger<GetTimedProgressQueryHandler> _logger;
        private readonly ITimedProgressService _timedProgressService;

        public GetTimedProgressQueryHandler(ILogger<GetTimedProgressQueryHandler> logger, ITimedProgressService timedProgressService)
        {
            _logger = logger;
            _timedProgressService = timedProgressService;
        }

        public async Task<IEnumerable<TimedProgressDto>> Handle(GetTimedProgressQuery request, CancellationToken cancellationToken) =>
            await _timedProgressService.GetRecordsForGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}