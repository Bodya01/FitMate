using FitMate.Core.UnitOfWork;
using MediatR;

namespace FitMate.Applcation.Commands.WorkoutPlan
{
    public class EditWorkoutPlanCommand : IRequest<Unit>
    {
        public Infrastructure.Entities.WorkoutPlan? WorkoutPlan { get; set; }
    }

    public class EditWorkoutPlanCommandHandler : IRequestHandler<EditWorkoutPlanCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditWorkoutPlanCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(EditWorkoutPlanCommand request, CancellationToken cancellationToken)
        {
            if (request.WorkoutPlan!.Id == Guid.Empty)
            {
                await _unitOfWork.WorkoutPlanRepository.Value.CreateAsync(request.WorkoutPlan);
            }
            else
            {
                await _unitOfWork.WorkoutPlanRepository.Value.UpdateAsync(request.WorkoutPlan);
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
