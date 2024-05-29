namespace YourFitnessTracker.Infrastructure.Models.BodyweightRecord;

public record CreateBodyweightRecordModel(DateTime Date, float Weight, string UserId);