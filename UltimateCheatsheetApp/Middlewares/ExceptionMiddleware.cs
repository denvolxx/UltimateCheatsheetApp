using Common.Helpers;
using System.Net;
using System.Text.Json;

namespace UltimateCheatsheetApp.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context, IHostEnvironment hostEnvironment)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = hostEnvironment.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal server error");

                var serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, serializerOptions));
            }
        }
    }
}
