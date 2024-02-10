using HCE.Domain.ResponseModel;
using HCE.Interfaces.Domain.Response;
using HCE.Interfaces.Intergartions;
using HCE.Utility.CommomEnum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HCE.Domain.Intergartions
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        //public IntegrationService()
        //{
        //}

        public IntegrationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IResponseResult<TOutput>> GetHttpResponse<TOutput, TInput>(HttpVerb verb, string url, string endPoint, TInput input, bool throwException = false)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    if (_httpContextAccessor != null)
                    {
                        StringValues authorizationHeaderStringValues;
                        string authorizationHeader;
                        _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out authorizationHeaderStringValues);
                        authorizationHeader = authorizationHeaderStringValues.FirstOrDefault();
                        if (!string.IsNullOrEmpty(authorizationHeader))
                        {
                            var passedAuthorization = authorizationHeader.Trim().Split(' ');
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(passedAuthorization.First(), passedAuthorization.Last());
                        }
                    }

                    StringContent content = null;
                    HttpResponseMessage response = null;

                    switch (verb)
                    {
                        case HttpVerb.Get:
                            response = await client.GetAsync(endPoint);
                            break;
                        case HttpVerb.Post:
                            content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
                            response = await client.PostAsync(endPoint, content);
                            break;
                        case HttpVerb.Put:
                            content = new StringContent(JsonSerializer.Serialize(input), Encoding.UTF8, "application/json");
                            response = await client.PutAsync(endPoint, content);
                            break;
                        case HttpVerb.Delete:
                            response = await client.DeleteAsync(endPoint);
                            break;
                        default:
                            break;
                    }

                    //response.EnsureSuccessStatusCode();
                    var result = new ResponseResult<TOutput>();
                    result.Status = response.StatusCode;
                    result.IsSuccess = response.IsSuccessStatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        try
                        {
                            string data = await response.Content.ReadAsStringAsync();
                            var serializeOptions = new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            };
                            result.Entity = JsonSerializer.Deserialize<TOutput>(data, serializeOptions);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, "");
                        }
                    }
                    else
                    {
                        result.Message = response.ReasonPhrase;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "");
                var result = new ResponseResult<TOutput>();
                result.Status = System.Net.HttpStatusCode.InternalServerError;
                result.IsSuccess = false;
                return result;
            }
        }
    }
}
