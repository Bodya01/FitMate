using FitMate.Data;
using FitMate.Handlers.Handlers.WorkoutPlan.Models.WorkoutPlan.Requests;
using FitMate.Handlers.Handlers.WorkoutPlan.Models.WorkoutPlan.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Handlers.Handlers.WorkoutPlan.Queries
{
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
