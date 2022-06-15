using CustomMiddleware.Helpers;
using Serilog;
using System.Text;

namespace CustomMiddleware.Middleware
{
    public class LoggerRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var logInfo = $"Request Method: {context.Request.Method} Path: {context.Request.Path} ";

            if (context.Request.QueryString.HasValue)
                logInfo += $"{context.Request.QueryString}";

            var bodyContent = await GetBodyContentAsync(context.Request);
            if (!string.IsNullOrEmpty(bodyContent))
                logInfo += $"Body: {bodyContent}";

            Log.Logger.Information(logInfo);

            await _next(context);
        }

        private async Task<string> GetBodyContentAsync(HttpRequest request)
        {
            if (request.ContentLength == 0)
                return null;

            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer).RemoveLineBreak();
        }
    }
}
