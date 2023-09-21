using FitMate.Core.Repositories.Interfaces;

namespace FitMate.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Lazy<IBodyweightRecordRepository> BodyweightRecordRepository { get; }
        Lazy<IBodyweightTargetRepository> BodyweightTargetRepository { get; }
        Lazy<IGoalRepository> GoalRepository { get; }
        Lazy<IGoalProgressRepository> GoalProgressRepository { get; }
        Lazy<IWorkoutPlanRepository> WorkoutPlanRepository { get; }
        Lazy<IFoodRepository> FoodRepository { get; }
        Lazy<IFoodRecordRepository> FoodRecordRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}