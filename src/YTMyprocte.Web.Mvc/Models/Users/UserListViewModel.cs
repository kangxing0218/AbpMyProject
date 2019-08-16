using System.Collections.Generic;
using YTMyprocte.Roles.Dto;
using YTMyprocte.Users.Dto;

namespace YTMyprocte.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
