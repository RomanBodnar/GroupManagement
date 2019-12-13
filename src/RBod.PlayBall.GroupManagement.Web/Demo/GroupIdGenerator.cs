using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RBod.PlayBall.GroupManagement.Web.Demo
{
    public class GroupIdGenerator : IGroupIdGenerator
    {
        private long currentId = 1;

        public long Next()
        {
            return ++this.currentId;
        }
    }
}
