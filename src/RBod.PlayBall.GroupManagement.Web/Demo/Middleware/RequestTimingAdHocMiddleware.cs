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
    public class RequestTimingAdHocMiddleware
    {
        private readonly RequestDelegate next;
        private int requestCount;

        public RequestTimingAdHocMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<RequestTimingAdHocMiddleware> logger)
        {
            var watch = Stopwatch.StartNew();
            await this.next(context);
            watch.Stop();
            Interlocked.Increment(ref requestCount);
            logger.LogTrace("Request {requestNumber} took {requestTime}ms", this.requestCount, watch.ElapsedMilliseconds);
        }
    }
}
