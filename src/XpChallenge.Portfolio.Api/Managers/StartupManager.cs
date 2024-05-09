using XpChallenge.Portfolio.Api.Middlawares.ExceptionMiddlaware;

namespace XpChallenge.Portfolio.Api.Managers
{
    public class StartupManager()
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
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
