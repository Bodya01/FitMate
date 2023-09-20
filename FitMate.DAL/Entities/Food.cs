using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitMate.DAL.Entities
{
    public class Food : IEntity
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Calories { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Carbohydrates { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Protein { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Fat { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int ServingSize { get; set; }
        [Required]
        public ServingUnit ServingUnit { get; set; }
    }
}
