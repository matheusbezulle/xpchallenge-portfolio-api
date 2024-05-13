using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace XpChallenge.Portfolio.Api.Configurations.Extensions
{
    public static class SwaggerConfigurationExtension
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                string path = PlatformServices.Default.Application.ApplicationBasePath;
                string name = PlatformServices.Default.Application.ApplicationName;
                string docPath = Path.Combine(path, $"{name}.xml");
                c.IncludeXmlComments(docPath);

                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "API de Gestão de Portfólios",
                    Description = "Este projeto é uma API .NET que permite cadastrar administradores no sistema, gerenciar portfólios de investimentos personalizados, e que notifica os administradores sobre produtos financeiros dos portfólios com vencimentos próximos."
                });
                
                c.MapType<int>(() => new OpenApiSchema() { Type = "integer", Default = new OpenApiInteger(0) });
                c.MapType<DateTime>(() => new OpenApiSchema() { Type = "datetime", Default = new OpenApiString("01/01/0001") });
                c.MapType<string>(() => new OpenApiSchema() { Type = "string", Default = new OpenApiString("") });
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
