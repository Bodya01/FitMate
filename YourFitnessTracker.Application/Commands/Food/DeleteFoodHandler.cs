using YourFitnessTracker.Business.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace YourFitnessTracker.Application.Commands.Food
{
    public record DeleteFood(Guid Id) : IRequest;

    internal sealed class DeleteFoodHandler : IRequestHandler<DeleteFood>
    {
        private readonly ILogger<DeleteFoodHandler> _logger;
        private readonly IFoodService _foodService;

        public DeleteFoodHandler(ILogger<DeleteFoodHandler> logger, IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        public async Task Handle(DeleteFood request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"A deletion of food with {request.Id} id begun");
            await _foodService.DeleteFoodAsync(request.Id, cancellationToken);
            _logger.LogInformation($"Food with id {request.Id} was successfull");
        }
    }
}