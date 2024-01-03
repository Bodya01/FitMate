using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Applcation.Commands.Bodyweight
{
    public record EditBodyweightRecordsCommand(DateTime[] RecordDates, float[] RecordWeights, string UserId) : IRequest;

    internal sealed class EditBodyweightRecordsCommandHandler : IRequestHandler<EditBodyweightRecordsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditBodyweightRecordsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EditBodyweightRecordsCommand command, CancellationToken cancellationToken)
        {
            var recordsToRemove = await _unitOfWork.BodyweightRecordRepository.Value.Get(e => e.UserId == command.UserId, s => s)
                .ToListAsync(cancellationToken);

            var records = command.RecordDates.Zip(command.RecordWeights, (date, weight) => new BodyweightRecord
            {
                UserId = command.UserId,
                Date = date,
                Weight = weight
            }).ToList();

            await _unitOfWork.BodyweightRecordRepository.Value.DeleteRangeAsync(recordsToRemove, cancellationToken);
            await _unitOfWork.BodyweightRecordRepository.Value.CreateRangeAsync(records, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}