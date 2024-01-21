using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos.Goals;
using MediatR;

namespace FitMate.Application.Queries.Goal.Weightlifting
{
    public record GetWeightliftingGoalQuery(Guid Id) : IRequest<WeightliftingGoalDto>
    {
        public string UserId { get; set; }
    }

    internal sealed class GetWeightliftingGoalQueryHandler : IRequestHandler<GetWeightliftingGoalQuery, WeightliftingGoalDto>
    {
        private readonly IWeightliftingGoalService _weightliftingGoalService;

        public GetWeightliftingGoalQueryHandler(IWeightliftingGoalService weightliftingGoalService)
        {
            _weightliftingGoalService = weightliftingGoalService;
        }

        public Task<WeightliftingGoalDto> Handle(GetWeightliftingGoalQuery request, CancellationToken cancellationToken) =>
            _weightliftingGoalService.GetWeightliftingGoalAsync(request.Id, request.UserId, cancellationToken);
    }
}