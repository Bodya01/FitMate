using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Applcation.Commands.WorkoutPlan
{
    public class EditWorkoutPlanCommand : IRequest<Unit>
    {
        public WorkoutPlanDto WorkoutPlan { get; set; }
    }

    public class EditWorkoutPlanCommandHandler : IRequestHandler<EditWorkoutPlanCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditWorkoutPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditWorkoutPlanCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Infrastructure.Entities.WorkoutPlan>(request.WorkoutPlan);

            if (request.WorkoutPlan!.Id == Guid.Empty) await _unitOfWork.WorkoutPlanRepository.Value.CreateAsync(entity);
            else await _unitOfWork.WorkoutPlanRepository.Value.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}