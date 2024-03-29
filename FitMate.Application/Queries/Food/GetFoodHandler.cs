﻿using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.Food
{
    public record GetFood(Guid FoodId) : IRequest<FoodDto>;

    internal sealed class GetFoodHandler : IRequestHandler<GetFood, FoodDto>
    {
        private readonly ILogger<GetFoodHandler> _logger;
        private readonly IFoodService _foodService;

        public GetFoodHandler(ILogger<GetFoodHandler> logger, IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        public async Task<FoodDto> Handle(GetFood request, CancellationToken cancellationToken) =>
            await _foodService.GetFoodByIdAsync(request.FoodId, cancellationToken);
    }
}