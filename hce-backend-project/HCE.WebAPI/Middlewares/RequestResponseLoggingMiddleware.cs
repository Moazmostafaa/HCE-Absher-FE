using HCE.Interfaces.UserResolverHandler;
using HCE.WebAPI.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HCE.WebAPI.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserResolverHandler userResolverHandler)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);

                Log.ForContext<RequestResponseLoggingMiddleware>().Information($"UserId:\"{ userResolverHandler.GetUserId() }\", {request}, {response}");

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            ////This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();
            var requestReader = new StreamReader(request.Body);
            var requestContent = await requestReader.ReadToEndAsync();
            request.Body.Position = 0;

            return $"HTTP request information:\n" +
                $"\tMethod: {request.Method}\n" +
                $"\tPath: {request.Path}\n" +
                $"\tQueryString: {request.QueryString}\n" +
                $"\tHeaders: {FormatHeaders(request.Headers)}\n" +
                $"\tSchema: {request.Scheme}\n" +
                $"\tHost: {request.Host}\n" +
                $"\tBody: {requestContent}";
            //return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {requestContent}";
        }

        private static string FormatHeaders(IHeaderDictionary headers) => string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));

        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string responseBodyText = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"HTTP request information:\n" +
                $"\tStatusCode: {response.StatusCode}\n" +
                $"\tContentType: {response.ContentType}\n" +
                $"\tHeaders: {FormatHeaders(response.Headers)}\n" +
                $"\tBody: {responseBodyText}";
            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            //return $"{response.StatusCode}: {responseBodyText}";
        }
    }
}
