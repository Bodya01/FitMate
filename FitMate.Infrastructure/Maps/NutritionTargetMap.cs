﻿using FitMate.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitMate.Infrastructure.Maps
{
    public sealed class NutritionTargetMap : IEntityTypeConfiguration<NutritionTarget>
    {
        public void Configure(EntityTypeBuilder<NutritionTarget> builder)
        {
            builder.ToTable("NutritionTargets");

            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id).ValueGeneratedOnAdd();

            builder.Property(n => n.DailyCalories).IsRequired();
            builder.Property(n => n.DailyProtein).IsRequired();
            builder.Property(n => n.DailyCarbohydrates).IsRequired();
            builder.Property(n => n.DailyFat).IsRequired();

            builder.HasOne(n => n.User)
                .WithMany(u => u.NutritionTargets)
                .HasForeignKey(n => n.UserId)
                .HasPrincipalKey(u => u.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
