using Core.Application.ComandQueryBus.Repositories;
using Core.Infrastructure.Repositories.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Registrations
{
    /// <summary>
    /// Registros de infraestructura necesarios para el CRUD de Automóvil.
    /// </summary>
    public static class InfraestructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext (SQL Server) usando la connection string "DefaultConnection"
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositorio de Automóvil (EF Core)
            services.AddScoped<IAutomovilRepository, AutomovilRepository>();

            return services;
        }
    }
}
