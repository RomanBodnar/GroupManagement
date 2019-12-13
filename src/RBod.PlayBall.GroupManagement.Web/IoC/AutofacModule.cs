using System;
using System.Collections.Generic;
using Autofac;
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
            builder.RegisterDecorator<IGroupsService>((context, service) => new GroupsServiceDecorator(service), "groupService");
        }

        private class GroupsServiceDecorator : IGroupsService
        {
            private readonly IGroupsService inner;

            public GroupsServiceDecorator(IGroupsService inner)
            {
                this.inner = inner;
            }

            public IReadOnlyCollection<Group> Get()
            {
                Console.WriteLine($"Hello from inner {nameof(Get)}");
                return this.inner.Get();
            }

            public Group GetById(long id)
            {
                Console.WriteLine($"Hello from inner {nameof(GetById)}");
                return this.inner.GetById(id);
            }

            public Group Update(Group @group)
            {
                Console.WriteLine($"Hello from inner {nameof(Update)}");
                return this.inner.Update(@group);
            }

            public Group Add(Group @group)
            {
                Console.WriteLine($"Hello from inner {nameof(Add)}");
                return this.inner.Add(@group);
            }
        }

    }
}