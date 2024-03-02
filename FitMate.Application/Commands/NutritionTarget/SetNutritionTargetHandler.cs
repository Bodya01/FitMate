using FitMate.Business.Interfaces;
using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Enums;
using FitMate.Infrastructure.Extensions;
using FitMate.Infrastructure.Models.NutritionTarget;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FitMate.Application.Commands.NutritionTarget
{
    public record SetNutritionTarget(int Weight, int Height, ActivityLevels ActivityLevel) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class SetNutritionTargetHandler : IRequestHandler<SetNutritionTarget>
    {
        private readonly ILogger<SetNutritionTargetHandler> _logger;
        private readonly INutritionTargetService _targetService;
        private readonly UserManager<FitnessUser> _userManager;

        public SetNutritionTargetHandler(ILogger<SetNutritionTargetHandler> logger, INutritionTargetService targetService, UserManager<FitnessUser> userManager)
        {
            _logger = logger;
            _targetService = targetService;
            _userManager = userManager;
        }

        public async Task Handle(SetNutritionTarget request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Setting nutrition target for user {request.UserId} begins");

            var user = await _userManager.FindByIdAsync(request.UserId);

            await _targetService.SetUserTargetAsync(
                new NutritionTargetCalculationParameters(request.Height, request.Weight, user.DateOfBirth.GetAge(), user.Gender, request.ActivityLevel),
                request.UserId,
                cancellationToken);

            _logger.LogInformation($"Nutrition target for user {request.UserId} was successfully set");
        }
    }
}