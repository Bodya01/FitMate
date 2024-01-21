using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos.Goals;
using MediatR;

namespace FitMate.Application.Queries.Goal.Weightlifting
{
    public record GetWeightliftingGoal(Guid Id) : IRequest<WeightliftingGoalDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWeightliftingGoalHandler : IRequestHandler<GetWeightliftingGoal, WeightliftingGoalDto>
    {
        private readonly IWeightliftingGoalService _weightliftingGoalService;

        public GetWeightliftingGoalHandler(IWeightliftingGoalService weightliftingGoalService)
        {
            _weightliftingGoalService = weightliftingGoalService;
        }

        public Task<WeightliftingGoalDto> Handle(GetWeightliftingGoal request, CancellationToken cancellationToken) =>
            _weightliftingGoalService.GetWeightliftingGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}