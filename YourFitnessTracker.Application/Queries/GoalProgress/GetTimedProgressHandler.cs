﻿using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos.GoalProgress;

namespace YourFitnessTracker.Application.Queries.GoalProgress
{
    public record GetTimedProgress(Guid Id) : IRequest<IEnumerable<TimedProgressDto>>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetTimedProgressHandler : FitMateRequestHandler<GetTimedProgress, IEnumerable<TimedProgressDto>>
    {
        private readonly ITimedProgressService _timedProgressService;

        public GetTimedProgressHandler(ITimedProgressService timedProgressService)
        {
            _timedProgressService = timedProgressService;
        }

        public override async Task<IEnumerable<TimedProgressDto>> Handle(GetTimedProgress request, CancellationToken cancellationToken) =>
            await _timedProgressService.GetRecordsForGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}