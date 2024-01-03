namespace FitMate.Infrastructure.Models.GoalProgress;

public record TimedProgressViewModel(string Date, TimeSpan Timespan, float Quantity, string QuantityUnit);