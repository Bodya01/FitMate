using AutoMapper;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Applcation.Commands.WorkoutPlan
{
    public record EditWorkoutPlanCommand(WorkoutPlanDto WorkoutPlan) : IRequest;

    internal sealed class EditWorkoutPlanCommandHandler : IRequestHandler<EditWorkoutPlanCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EditWorkoutPlanCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(EditWorkoutPlanCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Infrastructure.Entities.WorkoutPlan>(request.WorkoutPlan);

            if (request.WorkoutPlan!.Id == Guid.Empty) await _unitOfWork.WorkoutPlanRepository.Value.CreateAsync(entity, cancellationToken);
            else await _unitOfWork.WorkoutPlanRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}