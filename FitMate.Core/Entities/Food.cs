namespace FitMate.Infrastructure.Entities;

public class Food : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public int Calories { get; set; }
    public int Carbohydrates { get; set; }
    public int Protein { get; set; }
    public int Fat { get; set; }
    public int ServingSize { get; set; }
    public ServingUnit ServingUnit { get; set; }

    public ICollection<FoodRecord> FoodRecords { get; set; }
}
