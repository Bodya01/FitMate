using FitMate.Infrastucture.Dtos.Goals;
using System.Collections.Generic;

namespace FitMate.Presentation.ViewModels.Goal;

public sealed record GoalSummaryViewModel(IEnumerable<WeightliftingGoalDto> WeightLiftingGoals, IEnumerable<TimedGoalDto> TimedGoals);