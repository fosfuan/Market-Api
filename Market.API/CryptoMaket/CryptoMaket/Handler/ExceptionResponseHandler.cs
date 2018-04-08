using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CryptoMaket.Handler
{
    public class ExceptionResponseHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionResponseHandler> logger;

        public ExceptionResponseHandler(RequestDelegate next, ILogger<ExceptionResponseHandler> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var response = context.Response;

            switch (response.StatusCode)
            {
                case 404:
                    code = HttpStatusCode.NotFound;
                    break;
                case 400:
                    code = HttpStatusCode.BadRequest;
                    break;
                case 403:
                    code = HttpStatusCode.Forbidden;
                    break;
                case 401:
                    code = HttpStatusCode.Unauthorized;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;           

            }

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            logger.LogError(ex, ex.Message);
            return response.WriteAsync(result);
        }
    }
}
