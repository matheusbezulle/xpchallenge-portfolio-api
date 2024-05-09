namespace XpChallenge.Portfolio.Api.Managers
{
    public class StartupManager()
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseRouting();
            //app.UseHealthChecks();
            //app.UseMiddleware(typeof(ErrorMiddlaware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
