using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class CoffeeShopDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        
        public CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.ApnsToken).IsRequired(false);
            modelBuilder.Entity<User>().Property(u => u.FcmToken).IsRequired(false);

            // User Role
            modelBuilder.Entity<UserRole>().HasNoKey();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}