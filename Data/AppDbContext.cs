using Microsoft.EntityFrameworkCore;
using SixMinApi.Models;

namespace SixMinApi.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Command> Commands => Set<Command>();
        public DbSet<Platform> Platforms => Set<Platform>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship
            modelBuilder.Entity<Command>()
                .HasOne(c => c.Platform)
                .WithMany()
                .HasForeignKey(c => c.PlatformId);
        }
        
    }
}