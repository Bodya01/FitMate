using FitMate.Infrastucture.Dtos.Base;

namespace FitMate.Infrastucture.Dtos;

public record NutritionTargetDto(Guid Id, int DailyCalories, int DailyCarbohydrates, int DailyProtein, int DailyFat, UserDto User) : DtoBase;