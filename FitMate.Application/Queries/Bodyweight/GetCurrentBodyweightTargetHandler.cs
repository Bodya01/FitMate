﻿using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Exceptions;
using FitMate.Infrastucture.Dtos.Base;
using MediatR;

namespace FitMate.Application.Queries.Bodyweight
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