using FitMate.Infrastructure.Entities;
using FitMate.Infrastructure.Entities.Interfaces;
using FitMate.Infrastructure.Maps;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitMate.Core.Context
{
    public sealed class FitMateContext : IdentityDbContext<FitnessUser>
    {
        public DbSet<BodyweightRecord> BodyweightRecords { get; set; }
        public DbSet<BodyweightTarget> BodyweightTargets { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodRecord> FoodRecords { get; set; }
        public DbSet<NutritionTarget> NutritionTargets { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<WeightliftingGoal> WeightliftingGoals { get; set; }
        public DbSet<TimedGoal> TimedGoals { get; set; }
        public DbSet<GoalProgress> GoalProgressRecords { get; set; }
        public DbSet<WeightliftingProgress> WeightliftingProgressRecords { get; set; }
        public DbSet<TimedProgress> TimedProgressRecords { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }

        public FitMateContext(DbContextOptions<FitMateContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new BodyweightRecordMap());
            builder.ApplyConfiguration(new BodyweightTargetMap());
            builder.ApplyConfiguration(new FoodMap());
            builder.ApplyConfiguration(new FoodRecordMap());
            builder.ApplyConfiguration(new GoalMap());
            builder.ApplyConfiguration(new GoalProgressMap());
            builder.ApplyConfiguration(new NutritionTargetMap());
            builder.ApplyConfiguration(new WorkoutPlanMap());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AuditEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AuditEntities()
        {
            var auditedEntities = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditedEntity && e.State == EntityState.Added)
                .Select(e => e.Entity as IAuditedEntity)
                .ToList();

            var currentTime = DateTime.UtcNow;

            foreach (var auditedEntity in auditedEntities)
            {
                auditedEntity.CreatedAt = currentTime;
            }
        }
    }
}