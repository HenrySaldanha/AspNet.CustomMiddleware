This repository has the objective of presenting the creation and configuration of a custom Middleware.

## Settings

In my **Startup** class I added the following code snippets:
   

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
	    app.UseLoggerMiddleware();
	    ...
    }
    
I created a class called **MiddlewareExtensions**, so that I can have a single file with the configuration of many middleware in the builder.

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerRequestMiddleware>();
        }
    }

## Middleware class implementation
I created the **LoggerRequestMiddleware** class for the middleware implementation. The purpose of this middleware is to log some attributes of an API request.

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
            if (request.ContentLength == 0)
                return null;

            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer).RemoveLineBreak();
        }
    }





## This project was built with
* [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [Serilog](https://serilog.net/)
* ~~coffee, pizza and late nights~~

## My contacts
* [LinkedIn](https://www.linkedin.com/in/henry-saldanha-3b930b98/)
* [LeetCode](https://leetcode.com/user5265z/)