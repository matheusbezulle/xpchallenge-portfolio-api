using Hangfire;
using XpChallenge.Portfolio.Api.Configurations.Extensions;
using XpChallenge.Portfolio.Api.Middlawares.ExceptionMiddlaware;
using XpChallenge.Portfolio.Application.Services;

namespace XpChallenge.Portfolio.Api.Managers
{
    public class StartupManager()
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwaggerConfiguration();
            app.UseRouting();
            app.UseMiddleware(typeof(ExceptionMiddlaware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<AdministradorService>("NotificarProdutosProximosAoVencimento", administradorService =>
                administradorService.NotificarProdutosProximosAoVencimento(new CancellationToken()), Cron.Daily);
        }
    }
}
