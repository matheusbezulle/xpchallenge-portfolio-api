using Microsoft.AspNetCore.Mvc;
using XpChallenge.Portfolio.Api.Configurations.Converters;
using XpChallenge.Portfolio.Application.DataTransfer;

namespace XpChallenge.Portfolio.Api.Configurations.Extensions
{
    public static class ControllersConfigurationExtension
    {
        public static void AddControllersApiBehavior(this IServiceCollection services)
        {
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = (errorContext) =>
                    {
                        var erros = errorContext.ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(m => m.ErrorMessage)
                            .ToList();

                        var result = new ResponseBaseDto(true)
                        {
                            Mensagens = erros,
                            CorrelationId = errorContext.HttpContext.TraceIdentifier
                        };

                        return new BadRequestObjectResult(result);
                    };
                }).AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                });
        }
    }
}
