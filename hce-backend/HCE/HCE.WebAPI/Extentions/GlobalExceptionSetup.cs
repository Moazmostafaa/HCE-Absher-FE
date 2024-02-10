using HCE.Domain.ResponseModel;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace HCE.WebAPI.Extentions
{
    public static class GlobalExceptionSetup
    {
        public static void UseGlobalException(this IApplicationBuilder app/*, ILoggerFactory loggerFactory*/)
        {
            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                            if (errorFeature != null)
                            {
                                var exception = errorFeature.Error;

                                if (!(exception is ValidationException validationException))
                                {
                                    //logger.LogCritical(exception, exception.Message);
                                    Log.Fatal(exception, exception.Message);
                                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                    context.Response.AddApplicationError(exception.Message);

                                    var error = new ResponseResult<bool>
                                    {
                                        Entity = false,
                                        Status = HttpStatusCode.InternalServerError,
                                        Message = exception.Message,
                                        InnerMessage = exception.InnerException?.Message,
                                        IsSuccess = false,
                                    };
                                    await context.Response.WriteAsJsonAsync(error);
                                }
                                else
                                {
                                    //logger.LogInformation(exception.Message);
                                    Log.Information(exception, exception.Message);
                                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                    context.Response.ContentType = "application/json";
                                    var errors = string.Join(", ", validationException.Errors.Select(x => x.ErrorMessage).ToList());
                                    var result = new ResponseResult<bool>
                                    {
                                        Entity = false,
                                        Status = HttpStatusCode.BadRequest,
                                        Message = errors
                                    };
                                    await context.Response.WriteAsJsonAsync(result);
                                }
                            }
                        });
                });
        }
    }
}
