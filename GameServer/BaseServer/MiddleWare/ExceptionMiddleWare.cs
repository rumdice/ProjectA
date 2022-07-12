using Microsoft.AspNetCore.Http;
using Protocol;
using System.Net;


namespace ServerLib
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);

                if (ex is Error error)
                {
                    await HandleExceptionAsync(httpContext, error);
                }
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Error error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorResponse()
            {
                Error = error.code,
            }.ToString());
        }
    }
}
