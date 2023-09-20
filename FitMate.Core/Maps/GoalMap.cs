using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.DAL.Maps
{
    public class GoalMap : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder.ToTable("Goals");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Activity).IsRequired();

            builder.HasOne(g => g.User)
                .WithMany(u => u.Goals)
                .HasForeignKey(g => g.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}
