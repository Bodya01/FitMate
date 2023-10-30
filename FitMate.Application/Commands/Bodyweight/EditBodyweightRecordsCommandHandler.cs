using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Applcation.Commands.Bodyweight
{
    public record EditBodyweightRecordsCommand(DateTime[] RecordDates, float[] RecordWeights, string UserId) : IRequest;

    public class EditBodyweightRecordsCommandHandler : IRequestHandler<EditBodyweightRecordsCommand>
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
            await _unitOfWork.BodyweightRecordRepository.Value.DeleteRangeAsync(recordsToRemove);

            var records = new List<BodyweightRecord>();

            for (var i = 0; i < command.RecordDates.Length; i++)
            {
                var newRecord = new BodyweightRecord()
                {
                    UserId = command.UserId,
                    Date = command.RecordDates[i],
                    Weight = command.RecordWeights[i]
                };
                records.Add(newRecord);
            }

            await _unitOfWork.BodyweightRecordRepository.Value.CreateRangeAsync(records, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}