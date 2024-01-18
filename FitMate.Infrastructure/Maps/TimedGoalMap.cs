using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.Infrastructure.Maps
{
    public class TimedGoalMap : IEntityTypeConfiguration<TimedGoal>
    {
        public void Configure(EntityTypeBuilder<TimedGoal> builder)
        {
            builder.ToTable("TimedGoal");

            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.QuantityUnit).IsRequired();
        }
    }
}