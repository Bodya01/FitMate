using FitMate.Core.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Commands.BodyweightTarget
{
    public record EditBodyweightTargetCommand(float Weight, DateTime Date, string UserId) : IRequest;
    internal sealed class EditBodyweightTargetCommandHandler : IRequestHandler<EditBodyweightTargetCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditBodyweightTargetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EditBodyweightTargetCommand request, CancellationToken cancellationToken)
        {
            var newTarget = await _unitOfWork.BodyweightTargetRepository.Value.Get(e => e.UserId == request.UserId, s => s)
                .OrderByDescending(t => t.TargetDate)
                .FirstOrDefaultAsync(cancellationToken);

            newTarget ??= new Infrastructure.Entities.BodyweightTarget() { UserId = request.UserId };

            newTarget.TargetWeight = request.Weight;
            newTarget.TargetDate = request.Date;

            if (newTarget.Id == Guid.Empty) await _unitOfWork.BodyweightTargetRepository.Value.CreateAsync(newTarget, cancellationToken);
            else await _unitOfWork.BodyweightTargetRepository.Value.UpdateAsync(newTarget, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}