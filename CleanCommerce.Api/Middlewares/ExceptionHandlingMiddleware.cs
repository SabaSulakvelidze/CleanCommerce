using CleanCommerce.Application.Exceptions;
using Microsoft.AspNetCore.CookiePolicy;
using System.Net;
using System.Text.Json;

namespace CleanCommerce.Api.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                BadRequestException => (int) HttpStatusCode.BadRequest,
                UnauthorizedException => (int) HttpStatusCode.Unauthorized,
                NotFoundException => (int) HttpStatusCode.NotFound,
                _=> (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode,
                message = exception.Message
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);

        }


    }
}
