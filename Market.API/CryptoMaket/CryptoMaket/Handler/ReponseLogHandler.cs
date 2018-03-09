using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMaket.Handler
{
    public class ReponseLogHandler
    {
        private readonly RequestDelegate next;
        private readonly Logger logger = LogManager.GetLogger("ResponseLogging");

        public ReponseLogHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            Stream originalBody = context.Response.Body;

            try
            {
                using (var memStream = new MemoryStream())
                {
                        var request = context.Request;
                    context.Response.Body = memStream;

                    var sw = Stopwatch.StartNew();
                    await next(context);
                    sw.Stop();

                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd().ToString();

                    var message = new
                    {
                        request.Method,
                        request.Path,
                        responseBody,
                        timeElapsed = $"{sw.ElapsedMilliseconds}ms."
                    };

                    this.logger.Info(message);
                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }

            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response {text}";
        }
    }
}
