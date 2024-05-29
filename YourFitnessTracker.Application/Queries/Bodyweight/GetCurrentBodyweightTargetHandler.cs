using MediatR;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Exceptions;
using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Application.Queries.Bodyweight
{
    public record GetCurrentBodyweightTarget(string UserId) : IRequest<BodyweightTargetDto?>;

    internal sealed class GetCurrentBodyweightTargetHandler : IRequestHandler<GetCurrentBodyweightTarget, BodyweightTargetDto?>
    {
        private readonly IBodyweightTargetService _bodyweightTargetService;

        public GetCurrentBodyweightTargetHandler(IBodyweightTargetService bodyweightTargetService)
        {
            _bodyweightTargetService = bodyweightTargetService;
        }

        public async Task<BodyweightTargetDto?> Handle(GetCurrentBodyweightTarget request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bodyweightTargetService.GetCurrentTargetAsync(request.UserId, cancellationToken);
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }
    }
}