using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Infrastructure.Maps
{
    public sealed class BodyweightRecordMap : IEntityTypeConfiguration<BodyweightRecord>
    {
        public void Configure(EntityTypeBuilder<BodyweightRecord> builder)
        {
            builder.ToTable("BodyweightRecords");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedOnAdd();

            builder.Property(b => b.Date).IsRequired();
            builder.Property(b => b.Weight).IsRequired();

            builder.HasOne(b => b.User)
                .WithMany(u => u.BodyweightRecords)
                .HasForeignKey(b => b.UserId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}