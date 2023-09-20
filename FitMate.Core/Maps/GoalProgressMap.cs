using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.DAL.Maps
{
    public class GoalProgressMap : IEntityTypeConfiguration<GoalProgress>
    {
        public void Configure(EntityTypeBuilder<GoalProgress> builder)
        {
            builder.ToTable("GoalProgressRecords");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Date).IsRequired();

            builder.HasOne(g => g.Goal)
                .WithMany(g => g.GoalProgressRecords)
                .HasForeignKey(g => g.GoalId)
                .HasPrincipalKey(g => g.Id);

            builder.HasOne(g => g.User)
                .WithMany(u => u.GoalProgressRecords)
                .HasForeignKey(g => g.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}