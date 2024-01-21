using FitMate.Core.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Application.Commands.BodyweightTarget
{
    public record EditBodyweightTarget(float Weight, DateTime Date) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class EditBodyweightTargetHandler : IRequestHandler<EditBodyweightTarget>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditBodyweightTargetHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EditBodyweightTarget request, CancellationToken cancellationToken)
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