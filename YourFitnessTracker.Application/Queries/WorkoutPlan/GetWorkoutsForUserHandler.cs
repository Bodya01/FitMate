using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Dtos;

namespace YourFitnessTracker.Application.Queries.WorkoutPlan
{
    public record GetWorkoutsForUser(string UserId) : IRequest<IEnumerable<WorkoutPlanDto>>;

    internal sealed class GetWorkoutsForUserHandler : FitMateRequestHandler<GetWorkoutsForUser, IEnumerable<WorkoutPlanDto>>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutsForUserHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public override async Task<IEnumerable<WorkoutPlanDto>> Handle(GetWorkoutsForUser request, CancellationToken cancellationToken) =>
            await TryGetCollectionAsync(_workoutPlanService.GetWorkoutsAsync(request.UserId, cancellationToken));
    }
}