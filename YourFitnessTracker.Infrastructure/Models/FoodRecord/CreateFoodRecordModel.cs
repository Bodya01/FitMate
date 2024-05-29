namespace YourFitnessTracker.Infrastructure.Models.FoodRecord
{
    public record CreateFoodRecordModel(float Quantity, DateTime ConsumptionDate, Guid FoodId, string UserId);
}