﻿namespace YourFitnessTracker.Infrastructure.Models.WorkoutPlan;

public record CreateWorkoutPlanModel(string Name, string SessionsJson, string UserId);