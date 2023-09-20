using FitMate.Core.Repositories.Implementations;
using FitMate.Core.Repositories.Interfaces;
using FitMate.Data;

namespace FitMate.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public Lazy<IBodyweightRepository> BodyweightRepository { get; private set; }
        public Lazy<IGoalRepository> GoalRepository { get; private set; }

        public UnitOfWork(FitMateContext context)
        {
            BodyweightRepository = new Lazy<IBodyweightRepository>(new BodyweightRepository(context));
            GoalRepository = new Lazy<IGoalRepository>(new GoalRepository(context));
        }
    }
}