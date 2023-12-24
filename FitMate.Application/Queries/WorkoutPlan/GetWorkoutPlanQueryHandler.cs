using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Applcation.Queries.WorkoutPlan
{
    public record GetWorkoutPlanQuery(Guid Id) : IRequest<WorkoutPlanDto>;

    internal sealed class GetWorkoutPlanQueryHandler : IRequestHandler<GetWorkoutPlanQuery, WorkoutPlanDto>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutPlanQueryHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public async Task<WorkoutPlanDto> Handle(GetWorkoutPlanQuery request, CancellationToken cancellationToken)
        {
            var result = await _workoutPlanService.GetWorkoutAsync(request.Id, cancellationToken);
            return await Task.FromResult(result);
        }
    }
}