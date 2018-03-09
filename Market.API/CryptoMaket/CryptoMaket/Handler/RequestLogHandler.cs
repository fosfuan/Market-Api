using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoMaket.Extensions;
using System.Text;

namespace CryptoMaket.Handler
{
    public class RequestLogHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestLogHandler> logger;

        public RequestLogHandler(RequestDelegate next, ILogger<RequestLogHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var request = context.Request;
                var requestLogMessage = $"REQUEST:\n{request.Method} - {request.Path.Value}{request.QueryString}";
                requestLogMessage += $"\n ContentType: {request.ContentType ?? "Not specified"}";
                requestLogMessage += $"\n Host: {request.Host}";
                requestLogMessage += $"\n URL: {request.Path}";
                
                logger.LogInformation(requestLogMessage);
                await next(context);
                var response = context.Response;

                if(response == null)
                {
                    logger.LogInformation($"Request {request.Path} return response nul");
                }
                else
                {
                    
                    if (response.IsSuccessStatusCode())
                    {
                        this.LogResponseSuccess(response, request);
                    }
                    else
                    {
                        this.LogResponseError(response, request);
                    }
                }
                
                var responseLogMessage = $"\nRESPONSE:\nStatus Code: {response.StatusCode}";
                logger.LogInformation(responseLogMessage);
            }
            catch (Exception ex)
            {
                var errorMessage = $"REQUEST:\n{context.Request.Method} - {context.Request.Path.Value}{context.Request.QueryString}.";
                errorMessage += $"Request failed with message: {ex.Message}";
                logger.LogError(errorMessage);
                throw ex;
            }
        }

        private void LogResponseSuccess(HttpResponse response, HttpRequest request)
        {
            if(response.ContentLength != null)
            {
                logger.LogInformation($"Returnig {response.StatusCode} for request {request.Method} on URI: {request.Path}");
            }
            else
            {
                logger.LogInformation($"Returning {response.StatusCode} but content is null");
            }
        }

        private void LogResponseError(HttpResponse response, HttpRequest request)
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.AppendGeneralInfoError(response, request, stringBuilder);

            if(response.StatusCode == 500)
            {
                logger.LogError(stringBuilder.ToString());
            }
            else
            {
                logger.LogWarning(stringBuilder.ToString());
            }
        }

        private void AppendGeneralInfoError(HttpResponse response, HttpRequest request, StringBuilder sb)
        {
            if(response.HttpContext != null)
            {
                sb.Append($"Returnig {response.StatusCode} for request {request.Method} on URI: {request.Path}");
            }
            else
            {
                sb.Append($"Returning {response.StatusCode} but content is null");
            }
        }
        
    }
}
