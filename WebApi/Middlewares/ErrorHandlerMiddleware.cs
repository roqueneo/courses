using System;
using System.Net;
using System.Threading.Tasks;
using Application.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        private readonly RequestDelegate _next;

        public readonly ILogger<ErrorHandlerMiddleware> _logger;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HanldeEceptionAsync(context, ex, _logger);
            }
        }

        private async Task HanldeEceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlerMiddleware> logger)
        {
            object errors = null;
            switch (ex)
            {
                case ErrorHandler ce:
                    logger.LogError(ce, "Custom error");
                    errors = ce.Errors;
                    context.Response.StatusCode = (int)ce.StatusCode;
                    break;
                case Exception e:
                    logger.LogError(e, "Server error");
                    errors = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var serializedErrors = JsonConvert.SerializeObject(new {errors});
                await context.Response.WriteAsync(serializedErrors);
            }
        }
    }
}