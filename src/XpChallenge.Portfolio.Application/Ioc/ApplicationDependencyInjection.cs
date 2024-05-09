using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Ioc
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMapper();
            services.AddMediatR(mfg => mfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped<IPortfolioService, IPortfolioService>();
        }
    }
}
