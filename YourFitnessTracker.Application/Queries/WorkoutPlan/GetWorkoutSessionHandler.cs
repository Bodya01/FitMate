using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Application.Queries.WorkoutPlan
{
    public record GetWorkoutSession(Guid Id, int SessionId) : IRequest<WorkoutSessionDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWorkoutSessionHandler : FitMateRequestHandler<GetWorkoutSession, WorkoutSessionDto>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutSessionHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public override async Task<WorkoutSessionDto> Handle(GetWorkoutSession request, CancellationToken cancellationToken)
        {
            var workout = await _workoutPlanService.GetWorkoutAsync(request.Id, request.UserId, cancellationToken);

            if (request.SessionId < 0 || request.SessionId >= workout.Sessions.Count)
                throw new EntityNotFoundException($"Session with id {request.SessionId} was not found for workout plan {request.Id}");

            return workout.Sessions.ToList()[request.SessionId];
        }
    }
}