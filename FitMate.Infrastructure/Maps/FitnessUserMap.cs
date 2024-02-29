using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.Infrastructure.Maps
{
    public sealed class FitnessUserMap : IEntityTypeConfiguration<FitnessUser>
    {
        public void Configure(EntityTypeBuilder<FitnessUser> builder)
        {
            builder.Property(u => u.Gender)
                .IsRequired()
                .HasConversion(
                    v => v.ToString(),
                    v => (Genders)Enum.Parse(typeof(Genders), v));
        }
    }
}