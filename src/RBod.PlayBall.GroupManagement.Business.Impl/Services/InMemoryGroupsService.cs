using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RBod.PlayBall.GroupManagement.Business.Models;
using RBod.PlayBall.GroupManagement.Business.Services;

namespace RBod.PlayBall.GroupManagement.Business.Impl.Services
{
    public class InMemoryGroupsService : IGroupsService
    {
        private readonly List<Group> groups = new List<Group>();
        private long currentId = 0;

        public IReadOnlyCollection<Group> Get()
        {
            return this.groups.AsReadOnly();
        }

        public Group GetById(long id)
        {
            return this.groups.SingleOrDefault(x => x.Id == id);
        }

        public Group Update(Group @group)
        {
            var toUpdate = this.groups.SingleOrDefault(x => x.Id == @group.Id);
            if (toUpdate == null)
            {
                return null;
            }

            toUpdate.Name = @group.Name;
            return toUpdate;
        }

        public Group Add(Group @group)
        {
            group.Id = ++this.currentId;
            this.groups.Add(group);
            return group;
        }
    }
}