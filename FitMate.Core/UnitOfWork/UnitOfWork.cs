using FitMate.Core.Context;
using FitMate.Core.Repositories.Implementations;
using FitMate.Core.Repositories.Interfaces;

namespace FitMate.Core.UnitOfWork
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly FitMateContext _context;
        public Lazy<IBodyweightRecordRepository> BodyweightRecordRepository { get; private set; }
        public Lazy<IBodyweightTargetRepository> BodyweightTargetRepository { get; private set; }
        public Lazy<IGoalRepository> GoalRepository { get; private set; }
        public Lazy<IGoalProgressRepository> GoalProgressRepository { get; private set; }
        public Lazy<IWorkoutPlanRepository> WorkoutPlanRepository { get; private set; }
        public Lazy<IFoodRepository> FoodRepository { get; private set; }
        public Lazy<IFoodRecordRepository> FoodRecordRepository { get; private set; }
        public Lazy<INutritionTargetRepository> NutritionTargetRepository { get; private set; }

        public UnitOfWork(FitMateContext context)
        {
            _context = context;
            BodyweightRecordRepository = new Lazy<IBodyweightRecordRepository>(new BodyweightRecordRepository(context));
            BodyweightTargetRepository = new Lazy<IBodyweightTargetRepository>(new BodyweightTargetRepository(context));
            GoalRepository = new Lazy<IGoalRepository>(new GoalRepository(context));
            GoalProgressRepository = new Lazy<IGoalProgressRepository>(new GoalProgressRepository(context));
            WorkoutPlanRepository = new Lazy<IWorkoutPlanRepository>(new WorkoutPlanRepository(context));
            FoodRepository = new Lazy<IFoodRepository>(new FoodRepository(context));
            FoodRecordRepository = new Lazy<IFoodRecordRepository>(new FoodRecordRepository(context));
            NutritionTargetRepository = new Lazy<INutritionTargetRepository>(new NutritionTargetRepository(context));
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}