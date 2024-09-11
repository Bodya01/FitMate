using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos.Goals;

namespace YourFitnessTracker.Application.Queries.Goal.Weightlifting
{
    public record GetWeightliftingGoal(Guid Id) : IRequest<WeightliftingGoalDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWeightliftingGoalHandler : FitMateRequestHandler<GetWeightliftingGoal, WeightliftingGoalDto>
    {
        private readonly IWeightliftingGoalService _weightliftingGoalService;

        public GetWeightliftingGoalHandler(IWeightliftingGoalService weightliftingGoalService)
        {
            _weightliftingGoalService = weightliftingGoalService;
        }

        public override async Task<WeightliftingGoalDto> Handle(GetWeightliftingGoal request, CancellationToken cancellationToken) =>
            await _weightliftingGoalService.GetWeightliftingGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}