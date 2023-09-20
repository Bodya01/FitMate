using System.ComponentModel.DataAnnotations;

namespace FitMate.Infrastructure.Entities;

public class FoodRecord : IEntity
{
    public long Id { get; set; }
    
    public DateTime ConsumptionDate { get; set; }
    public float Quantity { get; set; }

    public string UserId { get; set; }
    public long FoodId { get; set; }

    public FitnessUser User { get; set; }
    public Food Food { get; set; }
}
