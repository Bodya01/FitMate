using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;
using MediatR;

namespace YourFitnessTracker.Applcation.Queries.WorkoutPlan
{
    public record GetWorkoutsForUser(string UserId) : IRequest<List<WorkoutPlanDto>>;

    internal sealed class GetWorkoutsForUserHandler : IRequestHandler<GetWorkoutsForUser, List<WorkoutPlanDto>>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutsForUserHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public async Task<List<WorkoutPlanDto>> Handle(GetWorkoutsForUser request, CancellationToken cancellationToken) =>
            (await _workoutPlanService.GetWorkoutsAsync(request.UserId, cancellationToken)).ToList();
    }
}