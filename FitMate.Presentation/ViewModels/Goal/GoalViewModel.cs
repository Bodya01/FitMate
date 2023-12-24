using FitMate.Infrastructure.Entities;
using System.Collections.Generic;

namespace FitMate.Presentation.ViewModels.Goal
{
    internal sealed class GoalViewModel
    {
        public Infrastructure.Entities.Goal Goal { get; set; }
        public List<GoalProgress> Progress { get; set; }
    }
}