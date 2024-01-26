﻿using FitMate.Business.Interfaces;
using FitMate.Infrastucture.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Queries.Food
{
    public record GetAllFoods() : IRequest<IEnumerable<FoodDto>>;

    internal sealed class GetAllFoodsHandler : IRequestHandler<GetAllFoods, IEnumerable<FoodDto>>
    {
        private readonly ILogger<GetAllFoodsHandler> _logger;
        private readonly IFoodService _foodService;

        public GetAllFoodsHandler(ILogger<GetAllFoodsHandler> logger, IFoodService foodService)
        {
            _logger = logger;
            _foodService = foodService;
        }

        public async Task<IEnumerable<FoodDto>> Handle(GetAllFoods request, CancellationToken cancellationToken) =>
            await _foodService.GetAllFoodsAsync(cancellationToken);
    }
}