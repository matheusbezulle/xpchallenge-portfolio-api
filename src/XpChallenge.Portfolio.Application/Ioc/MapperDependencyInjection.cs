using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace XpChallenge.Portfolio.Application.Ioc
{
    public static class MapperDependencyInjection
    {
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();

            return services;
        }
    }
}
