using Core.Model.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // public DbSet<Pokemon> Pokemons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aşağıdaki konfigurasyonları uygula
            // modelBuilder.ApplyConfiguration(new ProductConfiguration());
            // modelBuilder.ApplyConfiguration(new ProductSeed(new[] {1, 2}));

            // Konfigurasyonlar burada da tanımlanabilyor.
            // Aşağıdakide bunun için örnek teşkil eder.
            // modelBuilder.Entity<Person>().HasKey(x => x.Id);
            // modelBuilder.Entity<Person>().Property(x => x.Id).UseIdentityColumn();
            // modelBuilder.Entity<Person>().Property(x => x.Name).HasMaxLength(100);
            // modelBuilder.Entity<Person>().Property(x => x.SurName).HasMaxLength(100);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().Property(a => a.FullName)
                .HasMaxLength(70)
                .IsRequired();
        }
    }
}