using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PersistentUnreal.ViewModels;
using System;
using System.Threading.Tasks;

namespace PersistentUnreal.Helpers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate m_next;
        private readonly ILogger<ErrorHandlingMiddleware> m_logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            m_next = next;
            m_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await m_next.Invoke(context);
            }
            catch(Exception ex)
            {
                m_logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
            }

            if(!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json";

                var response = new BaseApiResponse(context.Response.StatusCode);
                var json = JsonConvert.SerializeObject(response, new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

                await context.Response.WriteAsync(json);
            }
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
