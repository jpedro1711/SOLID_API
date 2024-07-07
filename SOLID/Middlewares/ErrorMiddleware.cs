using System.Net.Mime;
using System.Net;
using System.Text.Json;
using SOLID.Middlewares.ErrorResponses;

namespace SOLID.Middlewares
{
    public class ErrorMiddleware
    {
        private RequestDelegate _next;
        private ILogger _log;

        public ErrorMiddleware(RequestDelegate next, ILoggerFactory log)
        {
            _next = next;
            _log = log.CreateLogger("Error handler");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, ex.Message);
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            ErrorResponseModel response = new ErrorResponseModel(context.Response.StatusCode, e.Message);
            var logMsg = DateTime.Now + ", " + context.Response.StatusCode + ", " + e.Message;
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
