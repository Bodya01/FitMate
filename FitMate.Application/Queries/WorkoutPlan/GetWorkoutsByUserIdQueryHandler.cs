using AutoMapper;
using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Applcation.Queries.WorkoutPlan
{
    public class GetWorkoutByUserIdQuery : IRequest<GetWorkoutByUserIdResponse>
    {
        public string UserId { get; set; }
    }

    public class GetWorkoutByUserIdResponse
    {
        public List<WorkoutPlanDto> WorkoutPlans { get; set; }
    }

    public class GetWorkoutsByUserIdQueryHandler : IRequestHandler<GetWorkoutByUserIdQuery, GetWorkoutByUserIdResponse>
    {
        private readonly IWorkoutPlanService _workoutPlanService;
        private readonly IMapper _mapper;

        public GetWorkoutsByUserIdQueryHandler(IWorkoutPlanService workoutPlanService, IMapper mapper)
        {
            _workoutPlanService = workoutPlanService;
            _mapper = mapper;
        }

        public async Task<GetWorkoutByUserIdResponse> Handle(GetWorkoutByUserIdQuery request, CancellationToken cancellationToken)
        {
            var plans = await _workoutPlanService.GetWorkoutsAsync(request.UserId, 1, 100, cancellationToken);

            var response = new GetWorkoutByUserIdResponse
            {
                WorkoutPlans = _mapper.Map<List<WorkoutPlanDto>>(plans)
            };

            return response;
        }
    }
}
