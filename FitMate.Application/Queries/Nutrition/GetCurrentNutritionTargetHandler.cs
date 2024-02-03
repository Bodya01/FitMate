using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;

namespace FitMate.Application.Queries.Nutrition
{
    public record GetCurrentNutritionTarget(string UserId) : IRequest<NutritionTargetDto>;

    internal sealed class GetCurrentNutritionTargetHandler : IRequestHandler<GetCurrentNutritionTarget, NutritionTargetDto>
    {
        private readonly INutritionTargetService _nutritionTargetService;

        public GetCurrentNutritionTargetHandler(INutritionTargetService nutritionTargetService)
        {
            _nutritionTargetService = nutritionTargetService;
        }

        public async Task<NutritionTargetDto> Handle(GetCurrentNutritionTarget request, CancellationToken cancellationToken) =>
            await _nutritionTargetService.GetTargetForUser(request.UserId, cancellationToken);
    }
}