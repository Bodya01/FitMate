using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.BodyweightRecord;
using MediatR;
using Microsoft.Extensions.Logging;

namespace YourFitnessTracker.Applcation.Commands.Bodyweight
{
    public record EditBodyweightRecords() : IRequest
    {
        public DateTime[] Dates { get; set; }
        public float[] Weights { get; set; }
        public string UserId { get; set; }
    }

    internal sealed class EditBodyweightRecordsHandler : IRequestHandler<EditBodyweightRecords>
    {
        private readonly ILogger<EditBodyweightRecordsHandler> _logger;
        private readonly IBodyweightRecordService _bodyweightRecordService;

        public EditBodyweightRecordsHandler(ILogger<EditBodyweightRecordsHandler> logger, IBodyweightRecordService bodyweightRecordService)
        {
            _logger = logger;
            _bodyweightRecordService = bodyweightRecordService;
        }

        public async Task Handle(EditBodyweightRecords command, CancellationToken cancellationToken)
        {
            command.Dates ??= Enumerable.Empty<DateTime>().ToArray();
            command.Weights ??= Enumerable.Empty<float>().ToArray();

            var records = command.Dates
                .Zip(command.Weights, (date, weight) => new UpdateBodyweightRecordModel(date, weight, command.UserId))
                .ToList();

            _logger.LogInformation($"Update of bodyweight records for user {command.UserId} begins");
            await _bodyweightRecordService.UpdateRangeAsync(records, command.UserId, cancellationToken);
            _logger.LogInformation($"Bodyweight records for user {command.UserId} were successfully updated");
        }
    }
}