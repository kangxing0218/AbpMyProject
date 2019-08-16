using System.Collections.Generic;
using YTMyprocte.Roles.Dto;

namespace YTMyprocte.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleListDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
