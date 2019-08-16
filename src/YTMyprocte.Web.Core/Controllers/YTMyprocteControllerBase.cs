using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace YTMyprocte.Controllers
{
    public abstract class YTMyprocteControllerBase: AbpController
    {
        protected YTMyprocteControllerBase()
        {
            LocalizationSourceName = YTMyprocteConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
