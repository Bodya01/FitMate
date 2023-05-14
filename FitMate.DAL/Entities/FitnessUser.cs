using Microsoft.AspNetCore.Identity;

namespace FitMate.DAL.Entities
{
    public class FitnessUser : IdentityUser, IEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}
