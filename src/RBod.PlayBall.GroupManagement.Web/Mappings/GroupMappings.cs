using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBod.PlayBall.GroupManagement.Business.Models;
using RBod.PlayBall.GroupManagement.Web.Models;

namespace RBod.PlayBall.GroupManagement.Web.Mappings
{
    public static class GroupMappings
    {
        public static GroupViewModel ToViewModel(this Group model)
        {
            return model != null ? new GroupViewModel { Id = model.Id, Name = model.Name} : null;
        }

        public static Group ToServiceModel(this GroupViewModel model)
        {
            return model != null ? new Group { Id = model.Id, Name = model.Name} : null;
        }

        public static IReadOnlyCollection<GroupViewModel> ToViewModels(this IReadOnlyCollection<Group> models)
        {
            if (models.Count == 0)
            {
                return Array.Empty<GroupViewModel>();
            }

            return models.Select(x => x.ToViewModel()).ToArray();
        }

        public static IReadOnlyCollection<Group> ToServiceModels(this IReadOnlyCollection<GroupViewModel> models)
        {
            if (models.Count == 0)
            {
                return Array.Empty<Group>();
            }

            return models.Select(x => x.ToServiceModel()).ToArray();
        }
    }
}
