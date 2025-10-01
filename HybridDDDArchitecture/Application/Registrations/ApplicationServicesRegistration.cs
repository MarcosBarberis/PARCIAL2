using Core.Application.ComandQueryBus.Handlers.Automovil;
using Core.Application.ComandQueryBus.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application.Registrations
{
    /// <summary>Registros de la capa de aplicación para el CRUD de Automóvil.</summary>
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // (Opcional) AutoMapper si tu template lo requiere
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

            services.AddScoped<CreateAutomovilHandler>();
            services.AddScoped<CreateAutomovilHandler>();
            services.AddScoped<UpdateAutomovilHandler>();
            services.AddScoped<GetAutomovilByIdHandler>();
            services.AddScoped<GetAutomovilesHandler>();
            services.AddScoped<DeleteAutomovilHandler>();
            services.AddScoped<GetAutomovilByChasisHandler>();

            return services;
        }
    }
}
