using YourFitnessTracker.Infrastructure.Entities.Interfaces;

namespace YourFitnessTracker.Infrastructure.Entities;

public class NutritionTarget : IEntity, IUserOwnedEntity
{
    public Guid Id { get; set; }

    public int DailyCalories { get; set; }
    public int DailyCarbohydrates { get; set; }
    public int DailyProtein { get; set; }
    public int DailyFat { get; set; }

    public string UserId { get; set; }
    public FitnessUser User { get; set; }
}