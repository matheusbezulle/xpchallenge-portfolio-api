﻿using Microsoft.Extensions.PlatformAbstractions;
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
                    Description = "TODO"
                });
                
                c.MapType<int>(() => new OpenApiSchema() { Type = "integer", Default = new OpenApiInteger(0) });
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
