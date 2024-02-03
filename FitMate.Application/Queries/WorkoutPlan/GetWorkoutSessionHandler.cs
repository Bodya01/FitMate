using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Application.Queries.WorkoutPlan
{
    public record GetWorkoutSession(Guid Id, int SessionId) : IRequest<WorkoutSessionDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWorkoutSessionHandler : IRequestHandler<GetWorkoutSession, WorkoutSessionDto>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutSessionHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public async Task<WorkoutSessionDto> Handle(GetWorkoutSession request, CancellationToken cancellationToken)
        {
            var workout = await _workoutPlanService.GetWorkoutAsync(request.Id, request.UserId, cancellationToken);

            if (request.SessionId < 0 || request.SessionId >= workout.Sessions.Count)
                throw new EntityNotFoundException($"Session with id {request.SessionId} was not found for workout plan {request.Id}");

            return workout.Sessions.ToList()[request.SessionId];
        }
    }
}