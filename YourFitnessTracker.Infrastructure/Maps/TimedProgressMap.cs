using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Infrastructure.Maps
{
    public sealed class TimedProgressMap : IEntityTypeConfiguration<TimedProgress>
    {
        public void Configure(EntityTypeBuilder<TimedProgress> builder)
        {
            builder.ToTable("TimedGoalProgress");

            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).ValueGeneratedOnAdd();

            builder.Property(g => g.Date).IsRequired();

            builder.Property(g => g.Quantity).IsRequired();

            builder.Property(g => g.Time).IsRequired();


            builder.HasOne(g => g.Goal)
                .WithMany(g => g.ProgressRecords)
                .HasForeignKey(g => g.GoalId)
                .HasPrincipalKey(g => g.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(g => g.User)
                .WithMany(u => u.TimedProgressRecords)
                .HasForeignKey(g => g.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}
