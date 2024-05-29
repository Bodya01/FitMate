using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Models.Food;
using YourFitnessTracker.Infrastucture.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace YourFitnessTracker.Application.Commands.Food
{
    public record CreateFood(string Name, int Calories, int Carbohydrates, int Protein, int Fat, int ServingSize, ServingUnit ServingUnit) : IRequest;

    internal sealed class CreateFoodHandler : IRequestHandler<CreateFood>
    {
        private readonly ILogger<CreateFoodHandler> _logger;
        private readonly IFoodService _foodService;

        public CreateFoodHandler(ILogger<CreateFoodHandler> logger, IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        public async Task Handle(CreateFood request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creation of food with name {request.Name} begins");
            await _foodService.CreateFoodAsync(
                new CreateFoodModel(request.Name, request.Calories, request.Carbohydrates, request.Protein, request.Fat, request.ServingSize, request.ServingUnit),
                cancellationToken);
            _logger.LogInformation($"Food with name {request.Name} was successfully created");
        }
    }
}