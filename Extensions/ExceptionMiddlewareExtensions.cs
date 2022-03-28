using AuthorizationModule.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AuthorizationModule.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = contextFeature.Error;

                    _log4net.Error($"{exception.Message} : {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType}");
                    if(contextFeature != null)
                    {
                        await context.Response.WriteAsync(new AuthResponse { 
                            Token = null,
                            Message = exception.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
