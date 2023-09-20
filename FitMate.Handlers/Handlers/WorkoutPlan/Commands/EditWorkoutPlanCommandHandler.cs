using FitMate.Data;
using FitMate.Handlers.Handlers.WorkoutPlan.Models.Requests;
using MediatR;

namespace FitMate.Handlers.Handlers.WorkoutPlan.Commands
{
    public class EditWorkoutPlanCommandHandler : IRequestHandler<EditWorkoutPlanCommand, Unit>
    {
        public FitMateContext _context { get; set; }

        public EditWorkoutPlanCommandHandler(FitMateContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditWorkoutPlanCommand request, CancellationToken cancellationToken)
        {
            if (request.WorkoutPlan.Id == 0)
            {
                _context.WorkoutPlans.Add(request.WorkoutPlan);
            }
            else
            {
                _context.WorkoutPlans.Update(request.WorkoutPlan);
            }

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
