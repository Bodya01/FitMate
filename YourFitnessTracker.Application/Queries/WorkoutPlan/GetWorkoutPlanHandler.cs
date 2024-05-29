using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;
using MediatR;

namespace YourFitnessTracker.Applcation.Queries.WorkoutPlan
{
    public record GetWorkoutPlan(Guid Id) : IRequest<WorkoutPlanDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWorkoutPlanHandler : IRequestHandler<GetWorkoutPlan, WorkoutPlanDto>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutPlanHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public async Task<WorkoutPlanDto> Handle(GetWorkoutPlan request, CancellationToken cancellationToken) =>
            await _workoutPlanService.GetWorkoutAsync(request.Id, request.UserId, cancellationToken);
    }
}