namespace FitMate.Application.Commands.FoodRecord
{
    public class EditFoodRecordsCommand
    {
        public DateTime Date { get; set; }
        public List<Guid> FoodIds { get; set; }
        public List<float> Quantities { get; set; }
    }

    internal sealed class EditFoodRecordsCommandHandler
    {
    }
}