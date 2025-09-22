using Core.Application.ComandQueryBus.Handlers.Automovil;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Registrations
{
    /// <summary>
    /// Registros mínimos de la capa de aplicación para el CRUD de Automóvil.
    /// </summary>
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // (Opcional) AutoMapper si tu template lo requiere en otros lugares
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

            // ❌ QUITADO: MediatR + CommandQueryBus + DummyEntity/EventBus
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            // services.AddScoped<ICommandQueryBus, MediatrCommandQueryBus>();
            // services.AddScoped<IDummyEntityApplicationService, DummyEntityApplicationService>();
            // services.AddPublishers();
            // services.AddSubscribers();

            // ✅ Nuestro handler para el POST /api/v1/automovil
            services.AddScoped<CreateAutomovilHandler>();

            return services;
        }
    }
}
