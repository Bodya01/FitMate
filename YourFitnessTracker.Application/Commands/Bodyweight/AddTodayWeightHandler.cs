using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.BodyweightRecord;
using MediatR;
using Microsoft.Extensions.Logging;

namespace YourFitnessTracker.Applcation.Commands.Bodyweight
{
    public record AddTodayWeight(float Weight) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class AddTodayWeightHandler : IRequestHandler<AddTodayWeight>
    {
        private readonly ILogger<AddTodayWeightHandler> _logger;
        private readonly IBodyweightRecordService _bodyweightRecordService;

        public AddTodayWeightHandler(ILogger<AddTodayWeightHandler> logger, IBodyweightRecordService bodyweightRecordService)
        {
            _bodyweightRecordService = bodyweightRecordService;
            _logger = logger;
        }

        public async Task Handle(AddTodayWeight request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creation of today weight for user {request.UserId} begins");
            await _bodyweightRecordService.CreateTodayRecordAsync(new CreateTodayBodyweightRecordModel(request.Weight, request.UserId), cancellationToken);
            _logger.LogInformation($"Today weight for user {request.UserId} was successfully created");
        }
    }
}