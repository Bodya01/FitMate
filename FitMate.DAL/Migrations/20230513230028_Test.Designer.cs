﻿// <auto-generated />
using System;
using FitMate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FitMate.Migrations
{
    [DbContext(typeof(FitMateContext))]
    [Migration("20230513230028_Test")]
    partial class Test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FitMate.Infrastructure.Entities.BodyweightRecord", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("BodyweightRecords");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.BodyweightTarget", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("TargetDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("TargetWeight")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("BodyweightTargets");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.FitnessUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.Food", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<int>("Carbohydrates")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Fat")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Protein")
                        .HasColumnType("int");

                    b.Property<int>("ServingSize")
                        .HasColumnType("int");

                    b.Property<int>("ServingUnit")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CreatedByID");

                    b.ToTable("UserFoods");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.FoodRecord", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("ConsumptionDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("FoodID")
                        .HasColumnType("bigint");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("FoodID");

                    b.HasIndex("UserId");

                    b.ToTable("FoodRecords");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.Goal", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Activity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Goals");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Goal");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.GoalProgress", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("GoalID")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("GoalID");

                    b.HasIndex("UserId");

                    b.ToTable("GoalProgressRecords");

                    b.HasDiscriminator<string>("Discriminator").HasValue("GoalProgress");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.NutritionTarget", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<int>("DailyCalories")
                        .HasColumnType("int");

                    b.Property<int>("DailyCarbohydrates")
                        .HasColumnType("int");

                    b.Property<int>("DailyFat")
                        .HasColumnType("int");

                    b.Property<int>("DailyProtein")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("NutritionTargets");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.WorkoutPlan", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SessionsJSON")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("WorkoutPlans");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.TimedGoal", b =>
                {
                    b.HasBaseType("FitMate.Infrastructure.Entities.Goal");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<string>("QuantityUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasDiscriminator().HasValue("TimedGoal");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.WeightliftingGoal", b =>
                {
                    b.HasBaseType("FitMate.Infrastructure.Entities.Goal");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("WeightliftingGoal");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.TimedProgress", b =>
                {
                    b.HasBaseType("FitMate.Infrastructure.Entities.GoalProgress");

                    b.Property<float>("Quantity")
                        .HasColumnType("real");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.HasDiscriminator().HasValue("TimedProgress");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.WeightliftingProgress", b =>
                {
                    b.HasBaseType("FitMate.Infrastructure.Entities.GoalProgress");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasDiscriminator().HasValue("WeightliftingProgress");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.BodyweightRecord", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.BodyweightTarget", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.Food", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.FoodRecord", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.Goal", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.GoalProgress", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.Goal", "Goal")
                        .WithMany()
                        .HasForeignKey("GoalID");

                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Goal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.NutritionTarget", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitMate.Infrastructure.Entities.WorkoutPlan", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FitMate.Infrastructure.Entities.FitnessUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
