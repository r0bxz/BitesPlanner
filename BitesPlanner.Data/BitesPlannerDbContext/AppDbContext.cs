using BitesPlanner.Data.entities;
using BitesPlanner.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitesPlanner.Data.BitesPlannerDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Meal> Meals { get; set; }
    }

}
