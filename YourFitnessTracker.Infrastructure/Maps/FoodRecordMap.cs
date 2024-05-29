using YourFitnessTracker.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YourFitnessTracker.Infrastructure.Maps
{
    public sealed class FoodRecordMap : IEntityTypeConfiguration<FoodRecord>
    {
        public void Configure(EntityTypeBuilder<FoodRecord> builder)
        {
            builder.ToTable("FoodRecords");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.ConsumptionDate).IsRequired();
            builder.Property(f => f.Quantity).IsRequired();

            builder.HasOne(f => f.Food)
                .WithMany(f => f.FoodRecords)
                .HasForeignKey(f => f.FoodId)
                .HasPrincipalKey(f => f.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(f => f.User)
                .WithMany(u => u.FoodRecords)
                .HasForeignKey(f => f.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
