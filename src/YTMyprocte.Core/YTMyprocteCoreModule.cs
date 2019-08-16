using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using YTMyprocte.Authorization.Roles;
using YTMyprocte.Authorization.Users;
using YTMyprocte.Configuration;
using YTMyprocte.Localization;
using YTMyprocte.MultiTenancy;
using YTMyprocte.Timing;

namespace YTMyprocte
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class YTMyprocteCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            YTMyprocteLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.是否启用多租户
            Configuration.MultiTenancy.IsEnabled = YTMyprocteConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YTMyprocteCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
