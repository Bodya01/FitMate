using FitMate.Business.Interfaces;
using FitMate.Core.UnitOfWork;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Models.BodyweightRecord;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitMate.Applcation.Commands.Bodyweight
{
    public record EditBodyweightRecords(DateTime[] Dates, float[] Weights) : IRequest
    {
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
            var records = command.Dates
                .Zip(command.Weights, (date, weight) => new UpdateBodyweightRecordModel(date, weight))
                .ToList();

            _logger.LogInformation($"Update of bodyweight records for user {command.UserId} begins");
            await _bodyweightRecordService.UpdateRangeAsync(records, cancellationToken);
            _logger.LogInformation($"Bodyweight records for user {command.UserId} were successfully updated");
        }
    }
}