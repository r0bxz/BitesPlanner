using BitesPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitesPlanner.Data.BitesPlannerDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanItem> PlanItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  base.OnModelCreating(modelBuilder);

          //  modelBuilder.Entity<PlanItem>()
            //    .HasKey(pi => new { pi.PlanId, pi.LineNumber });
        }
    }
}
