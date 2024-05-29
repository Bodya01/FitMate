using MediatR;
using Microsoft.Extensions.Logging;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.Food;
using YourFitnessTracker.Infrastucture.Enums;

namespace YourFitnessTracker.Application.Commands.Food
{
    public record UpdateFood(Guid Id, string Name, int Calories, int Carbohydrates, int Protein, int Fat, int ServingSize, ServingUnit ServingUnit) : IRequest;

    internal sealed class UpdateFoodHandler : IRequestHandler<UpdateFood>
    {
        private readonly ILogger<UpdateFoodHandler> _logger;
        private readonly IFoodService _foodService;

        public UpdateFoodHandler(ILogger<UpdateFoodHandler> logger, IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        public async Task Handle(UpdateFood request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update of food with id {request.Id} begins");
            await _foodService.UpdateFoodAsync(new UpdateFoodModel(
                request.Id, request.Name, request.Calories, request.Carbohydrates, request.Protein, request.Fat, request.ServingSize, request.ServingUnit),
                cancellationToken);
            _logger.LogInformation($"Food with id {request.Id} was successfully updated");
        }
    }
}