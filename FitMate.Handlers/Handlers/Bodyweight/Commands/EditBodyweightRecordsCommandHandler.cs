using FitMate.Infrastructure.Entities;
using FitMate.Data;
using FitMate.Handlers.Handlers.Bodyweight.Models.Requests;
using MediatR;

namespace FitMate.Handlers.Handlers.Bodyweight.Commands
{
    public class EditBodyweightRecordsCommandHandler : IRequestHandler<EditBodyweightRecordsCommand>
    {
        private readonly IBodyweightRepository _bodyweightRepository;

        public EditBodyweightRecordsCommandHandler(IBodyweightRepository bodyweightRepository)
        {
            _bodyweightRepository = bodyweightRepository;
        }

        public async Task Handle(EditBodyweightRecordsCommand command, CancellationToken cancellationToken)
        {
            await _bodyweightRepository.DeleteExistingRecords(command.User);

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

            await _bodyweightRepository.StoreBodyweightRecords(records);
        }
    }
}
