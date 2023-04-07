using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shopping.Framework.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Web
{
    public class ApiBaseException: Exception
    {
        public string Code;
        public ApiBaseException(string message):base(message)
        {
            Code = ResponseBaseCode.Fail;
        }
        public ApiBaseException(string code, string message) : base(message)
        {
            Code = code;
        }
    }
    public class ApiBaseExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiBaseExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ExceptionHandler(context, ex);
            }
            
        }
        private void ExceptionHandler(HttpContext context, Exception ex)
        {
            ResponseBase response = ResponseBase.ServerError;
            if (ex is ApiBaseException)
            {
                var apiExce = ex as ApiBaseException;
                response = new ResponseBase() {
                    Code = apiExce!.Code,
                    Message = apiExce.Message
                };
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.WriteAsJsonAsync(response);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.WriteAsJsonAsync(response);
            }
        }
    }
    public static class ApiBaseExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiBaseException(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiBaseExceptionMiddleware>();
        }
    }
}
