using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos;

namespace YourFitnessTracker.Application.Queries.Nutrition
{
    public record GetCurrentNutritionTarget(string UserId) : IRequest<NutritionTargetDto>;

    internal sealed class GetCurrentNutritionTargetHandler : FitMateRequestHandler<GetCurrentNutritionTarget, NutritionTargetDto>
    {
        private readonly INutritionTargetService _nutritionTargetService;

        public GetCurrentNutritionTargetHandler(INutritionTargetService nutritionTargetService)
        {
            _nutritionTargetService = nutritionTargetService;
        }

        public override async Task<NutritionTargetDto> Handle(GetCurrentNutritionTarget request, CancellationToken cancellationToken) =>
            await _nutritionTargetService.GetUserTargetAsync(request.UserId, cancellationToken);
    }
}