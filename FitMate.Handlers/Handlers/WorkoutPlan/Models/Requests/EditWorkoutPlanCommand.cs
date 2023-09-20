using MediatR;

namespace FitMate.Handlers.Handlers.WorkoutPlan.Models.Requests
{
    public class EditWorkoutPlanCommand : IRequest<Unit>
    {
        public Infrastructure.Entities.WorkoutPlan? WorkoutPlan { get; set; }
    }
}
