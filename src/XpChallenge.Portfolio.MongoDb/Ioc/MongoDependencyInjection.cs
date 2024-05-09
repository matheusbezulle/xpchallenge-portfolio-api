using Microsoft.Extensions.DependencyInjection;
using XpChallenge.Portfolio.MongoDb.Repositories;
using XpChallenge.Portfolio.MongoDb.Repositories.Interfaces;

namespace XpChallenge.Portfolio.Infra.MongoDb.Ioc
{
    public static class MongoDependencyInjection
    {
        public static void AddInfraMongo(this IServiceCollection services)
        {
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        }
    }
}
