using FitMate.Infrastructure.Entities;
using MediatR;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Core.UnitOfWork;

namespace FitMate.Applcation.Commands.Bodyweight
{
    public class EditBodyweightRecordsCommand : IRequest
    {
        public DateTime[] RecordDates { get; set; }
        public float[] recordWeights { get; set; }
        public FitnessUser User { get; set; }
    }

    public class EditBodyweightRecordsCommandHandler : IRequestHandler<EditBodyweightRecordsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditBodyweightRecordsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(EditBodyweightRecordsCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.BodyweightRecordRepository.Value.DeleteAllForUser(command.User.Id);

            var records = new List<BodyweightRecord>();

            for (int i = 0; i < command.RecordDates.Length; i++)
            {
                var newRecord = new BodyweightRecord()
                {
                    User = command.User,
                    Date = command.RecordDates[i],
                    Weight = command.recordWeights[i]
                };
                records.Add(newRecord);
            }

            await _unitOfWork.BodyweightRecordRepository.Value.AddRangeAsync(records);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
