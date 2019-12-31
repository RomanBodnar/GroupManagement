using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Logging;
using RBod.PlayBall.GroupManagement.Business.Impl.Services;
using RBod.PlayBall.GroupManagement.Business.Models;
using RBod.PlayBall.GroupManagement.Business.Services;

namespace RBod.PlayBall.GroupManagement.Web.IoC
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InMemoryGroupsService>().Named<IGroupsService>("groupService").SingleInstance();
            builder.RegisterDecorator<IGroupsService>((context, service) => new GroupsServiceDecorator(service, context.Resolve<ILogger<GroupsServiceDecorator>>()), "groupService");
        }

        private class GroupsServiceDecorator : IGroupsService
        {
            private readonly IGroupsService inner;
            private readonly ILogger<GroupsServiceDecorator> logger;

            public GroupsServiceDecorator(IGroupsService inner, ILogger<GroupsServiceDecorator> logger)
            {
                this.inner = inner;
                this.logger = logger;
            }

            public IReadOnlyCollection<Group> Get()
            {
                using var scope = this.logger.BeginScope("Decorator scope: {decorator}", nameof(GroupsServiceDecorator));
                this.logger.LogTrace("Hello from inner {decoratedMethod}", nameof(Get));
                this.logger.LogTrace("Good bye from inner {decoratedMethod}", nameof(Get));
                return this.inner.Get();

            }

            public Group GetById(long id)
            {
                this.logger.LogWarning("Hello from inner {decoratedMethod}", nameof(GetById));
                return this.inner.GetById(id);
            }

            public Group Update(Group @group)
            {
                this.logger.LogTrace("Hello from inner {decoratedMethod}", nameof(Update));
                return this.inner.Update(@group);
            }

            public Group Add(Group @group)
            {
                this.logger.LogTrace("Hello from inner {decoratedMethod}", nameof(Add));
                return this.inner.Add(@group);
            }
        }

    }
}