using MediatR;
using YourFitnessTracker.Application.Abstractions;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Application.Queries.Bodyweight
{
    public record GetCurrentBodyweightTarget(string UserId) : IRequest<BodyweightTargetDto?>;

    internal sealed class GetCurrentBodyweightTargetHandler : FitMateRequestHandler<GetCurrentBodyweightTarget, BodyweightTargetDto?>
    {
        private readonly IBodyweightTargetService _bodyweightTargetService;

        public GetCurrentBodyweightTargetHandler(IBodyweightTargetService bodyweightTargetService)
        {
            _bodyweightTargetService = bodyweightTargetService;
        }

        public override async Task<BodyweightTargetDto?> Handle(GetCurrentBodyweightTarget request, CancellationToken cancellationToken) =>
            await TryGetModelAsync(_bodyweightTargetService.GetCurrentTargetAsync(request.UserId, cancellationToken));
    }
}