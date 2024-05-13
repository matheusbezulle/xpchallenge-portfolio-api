using Microsoft.Extensions.DependencyInjection;
using XpChallenge.Portfolio.Infra.Mongo.Repositories;
using XpChallenge.Portfolio.Infra.Mongo.Repositories.Interfaces;
using XpChallenge.Portfolio.Mongo.Repositories;
using XpChallenge.Portfolio.Mongo.Repositories.Interfaces;

namespace XpChallenge.Portfolio.Infra.Mongo.Ioc
{
    public static class MongoDependencyInjection
    {
        public static void AddInfraMongo(this IServiceCollection services)
        {
            services.AddScoped<IAdministradorRepository, AdministradorRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        }
    }
}
