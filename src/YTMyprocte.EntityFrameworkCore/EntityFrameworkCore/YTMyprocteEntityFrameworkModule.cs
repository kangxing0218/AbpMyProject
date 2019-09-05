using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using YTMyprocte.EntityFrameworkCore.Seed;

namespace YTMyprocte.EntityFrameworkCore
{
    [DependsOn(
        typeof(YTMyprocteCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class YTMyprocteEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[]
        {
            new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
                   && level == LogLevel.Information, true)
        });
        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<YTMyprocteDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        YTMyprocteDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        YTMyprocteDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                    options.DbContextOptions.UseLoggerFactory(MyLoggerFactory);
                    options.DbContextOptions.EnableSensitiveDataLogging(true);       //logging 不加密 development使用 !
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YTMyprocteEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
