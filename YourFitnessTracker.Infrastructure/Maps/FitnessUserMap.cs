using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourFitnessTracker.Infrastructure.Entities;
using YourFitnessTracker.Infrastructure.Enums;

namespace YourFitnessTracker.Infrastructure.Maps
{
    public sealed class FitnessUserMap : IEntityTypeConfiguration<FitnessUser>
    {
        public void Configure(EntityTypeBuilder<FitnessUser> builder)
        {
            builder.Property(u => u.Height).IsRequired(false);

            builder.Property(u => u.Gender)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (Genders)Enum.Parse(typeof(Genders), v));
        }
    }
}