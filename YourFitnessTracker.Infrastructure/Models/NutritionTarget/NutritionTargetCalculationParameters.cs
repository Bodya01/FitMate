using YourFitnessTracker.Infrastructure.Enums;

namespace YourFitnessTracker.Infrastructure.Models.NutritionTarget;

public record NutritionTargetCalculationParameters(float Height, float Weight, int Age, Genders Gender, ActivityLevels ActivityLevel);