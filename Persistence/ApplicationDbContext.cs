using Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base (options)
        {

        }
        public DbSet<Salary> Salaries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
