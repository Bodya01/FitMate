using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.DAL.Maps
{
    public class BodyweightRecordMap : IEntityTypeConfiguration<BodyweightRecord>
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