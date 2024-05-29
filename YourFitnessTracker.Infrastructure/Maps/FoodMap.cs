using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourFitnessTracker.Infrastructure.Entities;

namespace YourFitnessTracker.Infrastructure.Maps
{
    public sealed class FoodMap : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable("Foods");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.Name).IsRequired();
            builder.Property(f => f.Calories).IsRequired();
            builder.Property(f => f.Carbohydrates).IsRequired();
            builder.Property(f => f.Protein).IsRequired();
            builder.Property(f => f.Fat).IsRequired();
            builder.Property(f => f.ServingSize).IsRequired();
            builder.Property(f => f.ServingUnit).IsRequired();

            builder.HasMany(f => f.FoodRecords)
                .WithOne(f => f.Food)
                .HasForeignKey(f => f.FoodId)
                .HasPrincipalKey(f => f.Id);
        }
    }
}
