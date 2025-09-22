using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Repositories.Sql
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Automovil> Automoviles => Set<Automovil>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var e = modelBuilder.Entity<Automovil>();

            e.ToTable("Automoviles");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();

            e.Property(x => x.Marca).IsRequired();
            e.Property(x => x.Modelo).IsRequired();
            e.Property(x => x.Color).IsRequired();
            e.Property(x => x.Fabricacion).IsRequired();
            e.Property(x => x.NumeroMotor).IsRequired();
            e.Property(x => x.NumeroChasis).IsRequired();

            e.HasIndex(x => x.NumeroMotor).IsUnique();
            e.HasIndex(x => x.NumeroChasis).IsUnique();
        }
    }
}
