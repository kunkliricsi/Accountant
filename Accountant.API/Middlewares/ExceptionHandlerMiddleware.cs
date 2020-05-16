using Accountant.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Accountant.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private static Task HandleException(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                AuthenticationException _ => HttpStatusCode.Unauthorized,
                EntityNotFoundException _ => HttpStatusCode.NotFound,
                EntityAlreadyExistsException _ => HttpStatusCode.Conflict,
                ArgumentException _ => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };

            var result = JsonConvert.SerializeObject(new { statusCode = code, errorMessage = ex.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            return context.Response.WriteAsync(result);
        }
    }
}
