using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Infrastructure.Repositories.Sql
{
    // EF usará esta factory EN TIEMPO DE DISEÑO (migraciones) y
    // NO intentará construir el Startup/Host ni resolver otros servicios.
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(
                    // Usá la misma conexión que en runtime.
                    // Si preferís, podés leer de env var o appsettings si querés,
                    // pero hardcodeado es suficiente para migraciones.
                    "Server=(localdb)\\MSSQLLocalDB;Database=HybridDDD_Autos;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True"
                )
                .Options;

            return new AppDbContext(options);
        }
    }
}
