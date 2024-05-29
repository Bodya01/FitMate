using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourFitnessTracker.Infrastructure.Entities;

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
