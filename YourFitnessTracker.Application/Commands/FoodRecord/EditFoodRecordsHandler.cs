using MediatR;
using Microsoft.Extensions.Logging;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.FoodRecord;

namespace YourFitnessTracker.Application.Commands.FoodRecord
{
    public record EditFoodRecords(DateTime Date, List<Guid>? FoodIds, List<float>? Quantities) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class EditFoodRecordsHandler : IRequestHandler<EditFoodRecords>
    {
        private readonly ILogger<EditFoodRecordsHandler> _logger;
        private readonly IFoodRecordService _foodRecordService;

        public EditFoodRecordsHandler(ILogger<EditFoodRecordsHandler> logger, IFoodRecordService foodRecordService)
        {
            _logger = logger;
            _foodRecordService = foodRecordService;
        }

        public async Task Handle(EditFoodRecords request, CancellationToken cancellationToken)
        {
            var foodIds = request.FoodIds ?? [];
            var quantities = request.Quantities ?? new List<float>();

            var records = new CreateFoodRecordModel[foodIds.Count];
            for (var i = 0; i < records.Length; i++)
            {
                var foodId = i < foodIds.Count ? foodIds[i] : Guid.Empty;
                var quantity = i < quantities.Count ? quantities[i] : 0;

                records[i] = new CreateFoodRecordModel(quantity, request.Date, foodId, request.UserId);
            }

            _logger.LogInformation($"Updating food records for user {request.UserId} and date {request.Date.ToShortDateString()} begins");
            await _foodRecordService.UpdateFoodRecordRangeAsync(records, request.UserId, request.Date, cancellationToken);
            _logger.LogInformation($"Food records for user {request.UserId} and date {request.Date.ToShortDateString()} were successfully updated");
        }
    }
}