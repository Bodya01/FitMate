using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Applcation.Queries.WorkoutPlan
{
    public record GetWorkoutForUserQuery(string UserId) : IRequest<List<WorkoutPlanDto>>;

    public class GetWorkoutsForUserQueryHandler : IRequestHandler<GetWorkoutForUserQuery, List<WorkoutPlanDto>>
    {
        private readonly IWorkoutPlanService _workoutPlanService;
        private readonly IMapper _mapper;

        public GetWorkoutsForUserQueryHandler(IWorkoutPlanService workoutPlanService, IMapper mapper)
        {
            _workoutPlanService = workoutPlanService;
            _mapper = mapper;
        }

        public async Task<List<WorkoutPlanDto>> Handle(GetWorkoutForUserQuery request, CancellationToken cancellationToken)
        {
            var plans = await _workoutPlanService.GetWorkoutsAsync(request.UserId, 1, 100, cancellationToken);
            return _mapper.Map<List<WorkoutPlanDto>>(plans);
        }
    }
}