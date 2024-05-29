using System.Collections.Generic;
using YourFitnessTracker.Infrastucture.Dtos.Goals;

namespace YourFitnessTracker.Presentation.ViewModels.Goal;

public sealed record GoalSummaryViewModel(IEnumerable<WeightliftingGoalDto> WeightLiftingGoals, IEnumerable<TimedGoalDto> TimedGoals);