using XpChallenge.Portfolio.Api.Configurations.Extensions;
using XpChallenge.Portfolio.Api.Managers;
using XpChallenge.Portfolio.Application.Ioc;

namespace XpChallenge.Portfolio.Api
{
    public class Startup(IConfiguration configuration)
    {
        private readonly StartupManager _startupManager = new();
        private readonly IConfiguration _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersApiBehavior();
            services.AddSwaggerConfiguration();
            //services.AddApiConfiguration();
            services.AddApplication();
            services.AddMongo(_configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            _startupManager.Configure(app);
        }
    }
}
