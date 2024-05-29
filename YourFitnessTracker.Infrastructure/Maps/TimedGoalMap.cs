using YourFitnessTracker.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YourFitnessTracker.Infrastructure.Maps
{
    public sealed class TimedGoalMap : IEntityTypeConfiguration<TimedGoal>
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