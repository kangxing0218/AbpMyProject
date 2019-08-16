using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YTMyprocte.Configuration;
using YTMyprocte.EntityFrameworkCore;
using YTMyprocte.Migrator.DependencyInjection;

namespace YTMyprocte.Migrator
{
    [DependsOn(typeof(YTMyprocteEntityFrameworkModule))]
    public class YTMyprocteMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public YTMyprocteMigratorModule(YTMyprocteEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(YTMyprocteMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                YTMyprocteConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YTMyprocteMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
