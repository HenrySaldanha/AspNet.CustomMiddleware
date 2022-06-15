using CustomMiddleware.Middleware;

namespace CustomMiddleware.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerRequestMiddleware>();
        }
    }
}
