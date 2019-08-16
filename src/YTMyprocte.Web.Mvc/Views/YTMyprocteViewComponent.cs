using Abp.AspNetCore.Mvc.ViewComponents;

namespace YTMyprocte.Web.Views
{
    public abstract class YTMyprocteViewComponent : AbpViewComponent
    {
        protected YTMyprocteViewComponent()
        {
            LocalizationSourceName = YTMyprocteConsts.LocalizationSourceName;
        }
    }
}
