using XpChallenge.Portfolio.Api.Configurations.Extensions;
using XpChallenge.Portfolio.Api.Middlawares.ExceptionMiddlaware;

namespace XpChallenge.Portfolio.Api.Managers
{
    public class StartupManager()
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerConfiguration();
            app.UseRouting();
            //app.UseHealthChecks();
            app.UseMiddleware(typeof(ExceptionMiddlaware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
