using FitMate.Core.Repositories.Interfaces;

namespace FitMate.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        Lazy<IBodyweightRepository> BodyweightRepository { get; }
        Lazy<IGoalRepository> GoalRepository { get; }  
    }
}