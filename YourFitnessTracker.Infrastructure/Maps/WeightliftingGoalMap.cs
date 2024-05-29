using YourFitnessTracker.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YourFitnessTracker.Infrastructure.Maps
{
    public sealed class WeightliftingGoalMap : IEntityTypeConfiguration<WeightliftingGoal>
    {
        public void Configure(EntityTypeBuilder<WeightliftingGoal> builder)
        {
            builder.ToTable("WeightliftingGoal");

            builder.Property(x => x.Weight).IsRequired();
            builder.Property(x => x.Reps).IsRequired();
        }
    }
}
