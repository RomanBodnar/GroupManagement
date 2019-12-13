using Microsoft.Extensions.DependencyInjection;
using RBod.PlayBall.GroupManagement.Business.Impl.Services;
using RBod.PlayBall.GroupManagement.Business.Services;

namespace RBod.PlayBall.GroupManagement.Web.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IGroupsService, InMemoryGroupsService>();
            return services;
        }
    }
}