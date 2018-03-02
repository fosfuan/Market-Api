using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
