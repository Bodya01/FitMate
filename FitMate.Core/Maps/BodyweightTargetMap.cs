using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.DAL.Maps
{
    public class BodyweightTargetMap : IEntityTypeConfiguration<BodyweightTarget>
    {
        public void Configure(EntityTypeBuilder<BodyweightTarget> builder)
        {
            builder.ToTable("BodyweightTargets");

            builder.HasKey(x => x.Id);

            builder.Property(b => b.TargetWeight).IsRequired();
            builder.Property(b => b.TargetDate).IsRequired();

            builder.HasOne(b => b.User)
                .WithMany(u => u.BodyweightTargets)
                .HasForeignKey(b => b.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}
