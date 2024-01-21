using FitMate.Core.Repositories.Interfaces;

namespace FitMate.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Lazy<IBodyweightRecordRepository> BodyweightRecordRepository { get; }
        Lazy<IBodyweightTargetRepository> BodyweightTargetRepository { get; }
        Lazy<ITimedGoalRepository> TimedGoalRepository { get; }
        Lazy<IWeightliftingGoalRepository> WeightliftingGoalRepository { get; }
        Lazy<ITimedProgressRepository> TimedProgressRepository { get; }
        Lazy<IWeightliftingProgressRepository> WeightliftingProgressRepository { get; }
        Lazy<IWorkoutPlanRepository> WorkoutPlanRepository { get; }
        Lazy<IFoodRepository> FoodRepository { get; }
        Lazy<IFoodRecordRepository> FoodRecordRepository { get; }
        Lazy<INutritionTargetRepository> NutritionTargetRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}