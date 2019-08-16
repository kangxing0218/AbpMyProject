using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;

namespace YTMyprocte.Web.Views
{
    public abstract class YTMyprocteRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected YTMyprocteRazorPage()
        {
            LocalizationSourceName = YTMyprocteConsts.LocalizationSourceName;
        }
    }
}
