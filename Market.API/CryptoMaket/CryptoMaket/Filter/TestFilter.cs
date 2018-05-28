using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoMaket.Filter
{
    public class TestFilter : IAsyncActionFilter
    {
        private readonly ILogger<TestFilter> logger;

        public TestFilter(ILogger<TestFilter> logger)
        {
            this.logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            this.logger.LogInformation("Before from Filter");

            await next.Invoke();

            this.logger.LogInformation("After from filter!");
        }
    }
}
