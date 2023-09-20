using FitMate.Infrastructure.Entities;
using MediatR;
using FitMate.Core.Repositories.Interfaces;

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
        private readonly IBodyweightRecordRepository _bodyweightRepository;

        public EditBodyweightRecordsCommandHandler(IBodyweightRecordRepository bodyweightRepository)
        {
            _bodyweightRepository = bodyweightRepository;
        }

        public async Task Handle(EditBodyweightRecordsCommand command, CancellationToken cancellationToken)
        {
            await _bodyweightRepository.DeleteAllForUser(command.User.Id);

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

            await _bodyweightRepository.AddRangeAsync(records);
        }
    }
}
