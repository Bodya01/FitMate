using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.Infrastructure.Maps
{
    public class BodyweightTargetMap : IEntityTypeConfiguration<BodyweightTarget>
    {
        public void Configure(EntityTypeBuilder<BodyweightTarget> builder)
        {
            builder.ToTable("BodyweightTargets");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.TargetWeight).IsRequired();
            builder.Property(b => b.TargetDate).IsRequired();

            builder.HasOne(b => b.User)
                .WithMany(u => u.BodyweightTargets)
                .HasForeignKey(b => b.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}
