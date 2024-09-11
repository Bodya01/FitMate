using YourFitnessTracker.Core.Context;
using YourFitnessTracker.Core.Repositories.Implementations;
using YourFitnessTracker.Core.Repositories.Interfaces;

namespace YourFitnessTracker.Core.UnitOfWork
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly YourFitnessTrackerContext _context;
        public Lazy<IBodyweightRecordRepository> BodyweightRecordRepository { get; private set; }
        public Lazy<IBodyweightTargetRepository> BodyweightTargetRepository { get; private set; }
        public Lazy<ITimedGoalRepository> TimedGoalRepository { get; private set; }
        public Lazy<IWeightliftingGoalRepository> WeightliftingGoalRepository { get; private set; }
        public Lazy<ITimedProgressRepository> TimedProgressRepository { get; private set; }
        public Lazy<IWeightliftingProgressRepository> WeightliftingProgressRepository { get; private set; }
        public Lazy<IWorkoutPlanRepository> WorkoutPlanRepository { get; private set; }
        public Lazy<IFoodRepository> FoodRepository { get; private set; }
        public Lazy<IFoodRecordRepository> FoodRecordRepository { get; private set; }
        public Lazy<INutritionTargetRepository> NutritionTargetRepository { get; private set; }
        public Lazy<IUserRepository> UserRepository { get; private set; }

        public UnitOfWork(YourFitnessTrackerContext context)
        {
            _context = context;
            BodyweightRecordRepository = new Lazy<IBodyweightRecordRepository>(new BodyweightRecordRepository(context));
            BodyweightTargetRepository = new Lazy<IBodyweightTargetRepository>(new BodyweightTargetRepository(context));
            TimedGoalRepository = new Lazy<ITimedGoalRepository>(new TimedGoalRepository(context));
            WeightliftingGoalRepository = new Lazy<IWeightliftingGoalRepository>(new WeightliftingGoalRepository(context));
            TimedProgressRepository = new Lazy<ITimedProgressRepository>(new TimedProgressRepository(context));
            WeightliftingProgressRepository = new Lazy<IWeightliftingProgressRepository>(new WeightliftingProgressRepository(context));
            WorkoutPlanRepository = new Lazy<IWorkoutPlanRepository>(new WorkoutPlanRepository(context));
            FoodRepository = new Lazy<IFoodRepository>(new FoodRepository(context));
            FoodRecordRepository = new Lazy<IFoodRecordRepository>(new FoodRecordRepository(context));
            NutritionTargetRepository = new Lazy<INutritionTargetRepository>(new NutritionTargetRepository(context));
            UserRepository = new Lazy<IUserRepository>(new UserRepository(context));
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken);
    }
}