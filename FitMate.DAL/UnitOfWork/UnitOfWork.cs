using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;

namespace FitMate.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FitMateContext _context;
        public Lazy<IBodyweightRecordRepository> BodyweightRecordRepository { get; private set; }
        public Lazy<IBodyweightTargetRepository> BodyweightTargetRepository { get; private set; }
        public Lazy<IGoalRepository> GoalRepository { get; private set; }
        public Lazy<IGoalProgressRepository> GoalProgressRepository { get; private set; }
        public Lazy<IWorkoutPlanRepository> WorkoutPlanRepository { get; private set; }

        public UnitOfWork(FitMateContext context,
            IBodyweightRecordRepository bodyweightRecordRepository,
            IBodyweightTargetRepository bodyweightTargetRepository,
            IGoalRepository goalRepository,
            IGoalProgressRepository goalProgressRepository,
            IWorkoutPlanRepository workoutPlanRepository)
        {
            _context = context;
            BodyweightRecordRepository = new Lazy<IBodyweightRecordRepository>(bodyweightRecordRepository);
            BodyweightTargetRepository = new Lazy<IBodyweightTargetRepository>(bodyweightTargetRepository);
            GoalRepository = new Lazy<IGoalRepository>(goalRepository);
            GoalProgressRepository = new Lazy<IGoalProgressRepository>(goalProgressRepository);
            WorkoutPlanRepository = new Lazy<IWorkoutPlanRepository>(workoutPlanRepository);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}