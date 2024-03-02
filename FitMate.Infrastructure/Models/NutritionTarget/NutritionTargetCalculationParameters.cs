using FitMate.Infrastructure.Enums;

namespace FitMate.Infrastructure.Models.NutritionTarget;

public record NutritionTargetCalculationParameters(float Height, float Weight, int Age, Genders Gender, ActivityLevels ActivityLevel);