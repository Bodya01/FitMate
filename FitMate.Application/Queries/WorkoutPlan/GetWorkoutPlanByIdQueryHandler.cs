using FitMate.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Applcation.Queries.WorkoutPlan
{
    public class GetWorkoutPlanByIdQuery : IRequest<GetWorkoutPlanByIdResponse>
    {
        public long Id { get; set; }
    }

    public class GetWorkoutPlanByIdResponse
    {
        public Infrastructure.Entities.WorkoutPlan? WorkoutPlan { get; set; }
    }

    public class GetWorkoutPlanByIdQueryHandler : IRequestHandler<GetWorkoutPlanByIdQuery, GetWorkoutPlanByIdResponse>
    {
        private readonly FitMateContext _context;

        public GetWorkoutPlanByIdQueryHandler(FitMateContext context)
        {
            _context = context;
        }

        public async Task<GetWorkoutPlanByIdResponse> Handle(GetWorkoutPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new GetWorkoutPlanByIdResponse
            {
                WorkoutPlan = await _context.WorkoutPlans.FirstOrDefaultAsync(p => p.Id == request.Id)
            };

            return await Task.FromResult(result);
        }
    }
}
