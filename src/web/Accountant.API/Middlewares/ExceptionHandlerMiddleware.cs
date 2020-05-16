using Accountant.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Accountant.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogWarning(ex, "");

            var code = ex switch
            {
                AuthenticationException _ => HttpStatusCode.Unauthorized,
                EntityNotFoundException _ => HttpStatusCode.NotFound,
                EntityAlreadyExistsException _ => HttpStatusCode.Conflict,
                ArgumentException _ => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };

            var result = JsonConvert.SerializeObject(new { error = ex.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
