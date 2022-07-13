using CustomMiddleware.Helpers;
using Serilog;

namespace CustomMiddleware.Middleware;
public class LoggerRequestMiddleware
{
    private readonly RequestDelegate _next;

    public LoggerRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var logInfo = $"Request Method: {context.Request.Method}, Path: {context.Request.Path}";

        if (context.Request.QueryString.HasValue)
            logInfo += $"{context.Request.QueryString}";

        var bodyContent = await GetBodyContentAsync(context.Request);
        if (!string.IsNullOrEmpty(bodyContent))
            logInfo += $", Body: {bodyContent}";

        Log.Logger.Information(logInfo);

        await _next(context);
    }

    private async Task<string> GetBodyContentAsync(HttpRequest request)
    {
        request.EnableBuffering();
        var bodyAsText = await new StreamReader(request.Body).ReadToEndAsync();
        request.Body.Position = 0;
        return bodyAsText.RemoveLineBreak();
    }
}