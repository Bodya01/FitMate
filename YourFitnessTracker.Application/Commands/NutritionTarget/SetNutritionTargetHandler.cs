using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using YourFitnessTracker.Business.Interfaces;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Enums;
using YourFitnessTracker.Infrastructure.Extensions;
using YourFitnessTracker.Infrastructure.Models.NutritionTarget;

namespace YourFitnessTracker.Application.Commands.NutritionTarget
{
    public record SetNutritionTarget(int Weight, int Height, ActivityLevels ActivityLevel) : IRequest
    {
        public string UserId { get; set; }
    }

    internal sealed class SetNutritionTargetHandler : IRequestHandler<SetNutritionTarget>
    {
        private readonly ILogger<SetNutritionTargetHandler> _logger;
        private readonly INutritionTargetService _targetService;
        private readonly IUserService _userService;
        private readonly UserManager<FitnessUser> _userManager;

        public SetNutritionTargetHandler(ILogger<SetNutritionTargetHandler> logger, INutritionTargetService targetService, UserManager<FitnessUser> userManager, IUserService userService)
        {
            _logger = logger;
            _targetService = targetService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task Handle(SetNutritionTarget request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            _logger.LogInformation($"Update of height for user {request.UserId} begins");
            await _userService.UpdateUserHeight(request.UserId, request.Height, cancellationToken);
            _logger.LogInformation($"Height for user {request.UserId} was updated successfully");

            _logger.LogInformation($"Setting nutrition target for user {request.UserId} begins");

            await _targetService.SetUserTargetAsync(
                new NutritionTargetCalculationParameters(request.Height, request.Weight, user.DateOfBirth.GetAge(), user.Gender, request.ActivityLevel),
                request.UserId,
                cancellationToken);

            _logger.LogInformation($"Nutrition target for user {request.UserId} was successfully set");
        }
    }
}