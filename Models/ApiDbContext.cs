using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using TheTowerAPI.Configurations;

namespace TheTowerAPI.Models
{
    public class ApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Record> Records { get; set; }

        public ApiDbContext() { }

        public ApiDbContext(DbContextOptions options) : base(options) { }
        
        /*
         Fluent API for EF Core
         Applying configurations to model builder
         All implementation of constraints and relations is in Configuration folder
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new LevelConfiguration());
            modelBuilder.ApplyConfiguration(new RecordConfiguration());
        }
    }
}