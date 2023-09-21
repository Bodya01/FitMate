using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Applcation.Queries.WorkoutPlan
{
    public class GetWorkoutPlanByIdQuery : IRequest<WorkoutPlanDto>
    {
        public Guid Id { get; set; }
    }

    public class GetWorkoutPlanByIdQueryHandler : IRequestHandler<GetWorkoutPlanByIdQuery, WorkoutPlanDto>
    {
        private readonly IWorkoutPlanService _workoutPlanService;

        public GetWorkoutPlanByIdQueryHandler(IWorkoutPlanService workoutPlanService)
        {
            _workoutPlanService = workoutPlanService;
        }

        public async Task<WorkoutPlanDto> Handle(GetWorkoutPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _workoutPlanService.GetWorkoutAsync(request.Id, cancellationToken);
            return await Task.FromResult(result);
        }
    }
}