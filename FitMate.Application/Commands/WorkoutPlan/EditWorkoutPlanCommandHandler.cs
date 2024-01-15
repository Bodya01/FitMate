using AutoMapper;
using FitMate.Core.UnitOfWork;
using MediatR;

namespace FitMate.Applcation.Commands.WorkoutPlan
{
    public record EditWorkoutPlanCommand(Guid Id, string Name, string SessionsJSON) : IRequest
    {
        public string UserId { get; set; }
    }

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
            var entity = new Infrastructure.Entities.WorkoutPlan
            {
                Id = request.Id,
                Name = request.Name,
                SessionsJSON = request.SessionsJSON,
                UserId = request.UserId
            };

            if (request.Id == Guid.Empty) await _unitOfWork.WorkoutPlanRepository.Value.CreateAsync(entity, cancellationToken);
            else await _unitOfWork.WorkoutPlanRepository.Value.UpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}