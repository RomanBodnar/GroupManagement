using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RBod.PlayBall.GroupManagement.Web.Demo.Middleware
{
    public class RequestTimingFactoryMiddleware : IMiddleware
    {
        private ILogger<RequestTimingFactoryMiddleware> logger;
        private int requestCount;

        public RequestTimingFactoryMiddleware(ILogger<RequestTimingFactoryMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var watch = Stopwatch.StartNew();
            await next(context);
            watch.Stop();
            Interlocked.Increment(ref requestCount);
            logger.LogTrace("Request {requestNumber} took {requestTime}ms", this.requestCount, watch.ElapsedMilliseconds);
        }
    }
}
