using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitMate.DAL.Entities
{
    public class FoodRecord : IEntity
    {
        public long Id { get; set; }
        [Required]
        public DateTime ConsumptionDate { get; set; }
        [Required]
        public float Quantity { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public FitnessUser User { get; set; }

        [Required]
        [ForeignKey("Food")]
        public long FoodID { get; set; }
        public Food Food { get; set; }
    }
}
