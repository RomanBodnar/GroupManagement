using System;
using System.Collections.Generic;
using System.Text;
using RBod.PlayBall.GroupManagement.Business.Models;

namespace RBod.PlayBall.GroupManagement.Business.Services
{
    public interface IGroupsService
    {
        IReadOnlyCollection<Group> Get();

        Group GetById(long id);

        Group Update(Group group);

        Group Add(Group group);
    }
}