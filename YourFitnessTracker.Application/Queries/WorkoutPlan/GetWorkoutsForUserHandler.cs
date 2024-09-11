using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Application.Queries.WorkoutPlan
{
    public record GetWorkoutsForUser(string UserId) : IRequest<List<WorkoutPlanDto>>;

    internal sealed class GetWorkoutsForUserHandler : FitMateRequestHandler<GetWorkoutsForUser, List<WorkoutPlanDto>>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutsForUserHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public override async Task<List<WorkoutPlanDto>> Handle(GetWorkoutsForUser request, CancellationToken cancellationToken) =>
            (await TryGetCollectionAsync(_workoutPlanService.GetWorkoutsAsync(request.UserId, cancellationToken))).ToList();
    }
}