using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace World.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class NullObjectMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public NullObjectMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;

            //loggerFactory.AddProvider(new DebugLoggerProvider((text, logLevel) => logLevel >= LogLevel.Debug));
            //loggerFactory
            //    .AddDebug(LogLevel.Debug)
            //    .AddConsole(LogLevel.Debug);
            _logger = loggerFactory.CreateLogger<NullObjectMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);

            if (httpContext.Response.StatusCode == 204 && httpContext.Request.Method == "GET")
            {
                _logger.LogWarning($"WARN: Content at '{httpContext.Request.Path}' is unavailable");
                httpContext.Response.StatusCode = 404;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class NullObjectHandlerExtensions
    {
        public static IApplicationBuilder UseNullObjectMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NullObjectMiddleware>();
        }
    }
}
