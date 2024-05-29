using YourFitnessTracker.Infrastucture.Dtos.Base;

namespace YourFitnessTracker.Infrastucture.Dtos;

public record NutritionTargetDto(Guid Id, int DailyCalories, int DailyCarbohydrates, int DailyProtein, int DailyFat, string UserId) : DtoBase;