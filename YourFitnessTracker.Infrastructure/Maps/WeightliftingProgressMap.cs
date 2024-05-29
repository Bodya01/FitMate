using YourFitnessTracker.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YourFitnessTracker.Infrastructure.Maps
{
    public sealed class WeightliftingProgressMap : IEntityTypeConfiguration<WeightliftingProgress>
    {
        public void Configure(EntityTypeBuilder<WeightliftingProgress> builder)
        {
            builder.ToTable("WeightliftingGoalProgress");

            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).ValueGeneratedOnAdd();

            builder.Property(g => g.Date).IsRequired();

            builder.Property(g => g.Weight).IsRequired();

            builder.Property(g => g.Reps).IsRequired();


            builder.HasOne(g => g.Goal)
                .WithMany(g => g.ProgressRecords)
                .HasForeignKey(g => g.GoalId)
                .HasPrincipalKey(g => g.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(g => g.User)
                .WithMany(u => u.WeightliftingProgressRecords)
                .HasForeignKey(g => g.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}
