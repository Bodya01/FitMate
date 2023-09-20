namespace FitMate.DAL.Entities
{
    public class BodyweightTarget : IEntity
    {
        public long Id { get; set; }

        public float TargetWeight { get; set; }
        public DateTime TargetDate { get; set; }

        public string UserId { get; set; }
        public FitnessUser User { get; set; }
    }
}