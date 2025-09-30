using Application;
using Application.Registrations;
using AutoMapper;
using Core.Application;
using Filters;
using Infrastructure.Registrations;
using Microsoft.OpenApi.Models;
using Core.Application.ComandQueryBus.Repositories;
using Core.Application.ComandQueryBus.Handlers.Automovil;
using Core.Infrastructure.Repositories.Sql;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddApplicationServices();
            services.AddInfrastructureServices(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hybrid Architecture Project", Version = "v1" });
            });
            //services.AddMvc().AddMvcOptions(options =>
            //{
            //    options.Filters.Add<BaseExceptionFilter>();
            //});
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hybrid Architecture Project v1");
                c.RoutePrefix = "swagger"; // navega a /swagger
            });

            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();

            }

            CustomMapper.Instance = app.ApplicationServices.GetRequiredService<IMapper>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowSpecificOrigin");
            //app.UseAuthentication();
            app.UseAuthorization();
            //UseEventBus(app);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void UseEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            // Aqui se registran las subscripciones a eventos del bus de eventos, vinculando
            //eventos con sus respectivos handlers
            eventBus.Subscribe<DummyEntityCreatedIntegrationEvent, DummyEntityCreatedIntegrationEventHandlerSub>();
        }
    }
}
