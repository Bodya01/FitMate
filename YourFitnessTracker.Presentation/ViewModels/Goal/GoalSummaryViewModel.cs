using YourFitnessTracker.Infrastucture.Dtos.Goals;
using System.Collections.Generic;

namespace YourFitnessTracker.Presentation.ViewModels.Goal;

public sealed record GoalSummaryViewModel(IEnumerable<WeightliftingGoalDto> WeightLiftingGoals, IEnumerable<TimedGoalDto> TimedGoals);