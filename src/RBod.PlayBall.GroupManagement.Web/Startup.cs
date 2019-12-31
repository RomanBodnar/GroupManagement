using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RBod.PlayBall.GroupManagement.Web.Demo.Middleware;
using RBod.PlayBall.GroupManagement.Web.IoC;

namespace RBod.PlayBall.GroupManagement.Web
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config)
        {
            this.config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            // default DI container:
            services.AddTransient<RequestTimingFactoryMiddleware>();

            services.AddBusiness();
            services.AddOptions();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware(typeof(RequestTimingAdHocMiddleware));
            app.UseMiddleware<RequestTimingFactoryMiddleware>();

            app.Map("/ping", builder =>
            {
                builder.UseMiddleware<RequestTimingFactoryMiddleware>();
                builder.Run(async (context) => { await context.Response.WriteAsync("pong from path"); });
            });

            app.MapWhen(context => context.Request.Headers.ContainsKey("ping"), builder =>
            {
                builder.UseMiddleware<RequestTimingFactoryMiddleware>();
                builder.Run(async (context) => { await context.Response.WriteAsync("pong from header"); });
            });

            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("X-Powered-By", "Roman Bodnar");
                    return Task.CompletedTask;
                });
               
                await next.Invoke();
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.Run(async (context) => { await context.Response.WriteAsync("No middleware can handle a request"); });
        }
    }
}
