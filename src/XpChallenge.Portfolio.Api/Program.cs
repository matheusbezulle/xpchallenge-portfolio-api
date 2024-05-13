using System.Globalization;

namespace XpChallenge.Portfolio.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var culture = new CultureInfo("pt-BR");
            culture.NumberFormat.NumberDecimalDigits = 2;
            CultureInfo.CurrentCulture = culture;

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Falha ao iniciar. Erro: {ex.Message}");
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

            return builder;
        }
    }
}
