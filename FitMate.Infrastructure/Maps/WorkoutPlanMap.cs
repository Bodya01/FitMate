using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.Infrastructure.Maps
{
    public sealed class WorkoutPlanMap : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder.ToTable("WorkoutPlans");

            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedOnAdd();

            builder.Property(w => w.Name).IsRequired();
            builder.Property(w => w.SessionsJSON).IsRequired();

            builder.HasOne(w => w.User)
                .WithMany(u => u.WorkoutPlans)
                .HasForeignKey(w => w.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}