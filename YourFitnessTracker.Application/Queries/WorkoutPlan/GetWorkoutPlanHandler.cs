using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Applcation.Queries.WorkoutPlan
{
    public record GetWorkoutPlan(Guid Id) : IRequest<WorkoutPlanDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWorkoutPlanHandler : FitMateRequestHandler<GetWorkoutPlan, WorkoutPlanDto>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutPlanHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public override async Task<WorkoutPlanDto> Handle(GetWorkoutPlan request, CancellationToken cancellationToken) =>
            await _workoutPlanService.GetWorkoutAsync(request.Id, request.UserId, cancellationToken);
    }
}