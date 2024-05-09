using System.Net;
using System.Text.Json;
using XpChallenge.Portfolio.Api.Responses;

namespace XpChallenge.Portfolio.Api.Middlawares.ExceptionMiddlaware
{
    public class ExceptionMiddlaware(RequestDelegate next, ILogger<ExceptionMiddlaware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddlaware> _logger = logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ResponseBase(true);
            response.Mensagens.Add(ex.Message);

            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }
    }
}
