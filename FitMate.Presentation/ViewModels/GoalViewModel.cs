using FitMate.Infrastructure.Entities;
using System.Collections.Generic;

namespace FitMate.ViewModels
{
    public class GoalViewModel
    {
        public Goal Goal { get; set; }
        public List<GoalProgress> Progress { get; set; }
    }
}