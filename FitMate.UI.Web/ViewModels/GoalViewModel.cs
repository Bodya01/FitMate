using FitMate.Infrastructure.Entities;

namespace FitMate.ViewModels
{
    public class GoalViewModel
    {
        public Goal Goal { get; set; }
        public GoalProgress[] Progress { get; set; }
    }
}
