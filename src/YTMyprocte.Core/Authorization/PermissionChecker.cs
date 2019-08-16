using Abp.Authorization;
using YTMyprocte.Authorization.Roles;
using YTMyprocte.Authorization.Users;

namespace YTMyprocte.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
