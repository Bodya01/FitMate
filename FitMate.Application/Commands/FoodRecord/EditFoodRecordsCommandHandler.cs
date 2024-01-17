using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Models.FoodRecord;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.FoodRecord
{
    public record EditFoodRecordsCommand(DateTime Date, List<Guid> FoodIds, List<float> Quantities) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class EditFoodRecordsCommandHandler : IRequestHandler<EditFoodRecordsCommand>
    {
        private readonly ILogger<EditFoodRecordsCommandHandler> _logger;
        private readonly IFoodRecordService _foodRecordService;

        public EditFoodRecordsCommandHandler(ILogger<EditFoodRecordsCommandHandler> logger, IFoodRecordService foodRecordService)
        {
            _logger = logger;
            _foodRecordService = foodRecordService;
        }

        public async Task Handle(EditFoodRecordsCommand request, CancellationToken cancellationToken)
        {
            var records = new CreateFoodRecordModel[request.FoodIds.Count];
            for (var i = 0; i < request.FoodIds.Count; i++)
                records[i] = new CreateFoodRecordModel(request.Quantities[i], request.Date, request.FoodIds[i], request.UserId);

            _logger.LogInformation($"Updating food records for user {request.UserId} and date {request.Date.ToShortDateString()} begins");
            await _foodRecordService.UpdateFoodRecordRangeAsync(records, request.UserId, request.Date, cancellationToken);
            _logger.LogInformation($"Food records for user {request.UserId} and date {request.Date.ToShortDateString()} were successfully updated");
        }
    }
}